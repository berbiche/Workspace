using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace TP1.Models
{
    public class Task
    {
        [Key, Display(Name = "Id")]
        public int Id { get; set; }


        [Required(ErrorMessage = "La description de la tâche est requise")]
        [Display(Name = "Description")]
        [StringLength(200)]
        public string Description { get; set; }


        [Required(ErrorMessage = "Le nom du client est requis!")]
        [Display(Name = "Client")]
        [StringLength(20, ErrorMessage = "Le nom du client ne peut pas être plus long que 20 charactères")]
        public string Client { get; set; }


        [Required(ErrorMessage = "La priorité de la tâche est nécessaire")]
        [Range(1, 3, ErrorMessage = "La valeur doit être comprise entre 1 et 3")]
        [Display(Name = "Priorité")]
        public int Priority { get; set; }


        [Required(ErrorMessage = "La date d'écheance est requise")]
        [DataType(DataType.DateTime, ErrorMessage = "Date invalide")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [CustomDateTime]
        [Display(Name = "Échéance")]
        public DateTime Due { get; set; }


        [Display(Name = "Date de création")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Creation { get; set; }


        [Display(Name = "Date terminée")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? ClosedDate { get; set; }


        private bool _done;


        [Display(Name = "État")]
        public bool Done
        {
            get { return _done; }
            set
            {
                if (value)
                    ClosedDate = DateTime.Now;
                else
                    ClosedDate = null;
                _done = value;
            }
        }

        public static List<Task> GetList()
        {
            string cStr = ConfigurationManager.ConnectionStrings["TaskManager"].ConnectionString;
            string requete = "SELECT * FROM Task";

            using (SqlConnection cnx = new SqlConnection(cStr))
            {
                using (SqlCommand cmd = new SqlCommand(requete, cnx))
                {
                    cnx.Open();
                    cmd.CommandType = System.Data.CommandType.Text;
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        List<Task> taskList = new List<Task>();
                        while (dataReader.Read())
                        {
                            Task t = new Task();
                            t.Id = (int)dataReader["id"];
                            t.Description = (string)dataReader["Description"];
                            t.Client = (string)dataReader["Client"];
                            t.Priority = (int)dataReader["Priority"];
                            t.Creation = (DateTime)dataReader["Creation"];
                            t.Due = (DateTime)dataReader["Due"];
                            if (!dataReader.IsDBNull(dataReader.GetOrdinal("ClosedDate")))
                            {
                                t.ClosedDate = (DateTime)dataReader["ClosedDate"];
                            }
                            t.Done = (Boolean)dataReader["Done"];
                            taskList.Add(t);
                        }
                        return taskList;
                    }
                }
            }
        }

        public static bool Destroy(int id)
        {
            string cN = ConfigurationManager.ConnectionStrings["TaskManager"].ConnectionString;
            string requete = "DELETE FROM Task WHERE Id = " + id;

            using (SqlConnection cnx = new SqlConnection(cN))
            {
                using (SqlCommand cmd = new SqlCommand(requete, cnx))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cnx.Open();
                    cmd.ExecuteNonQuery();
                    cnx.Close();
                    return true;
                }
            }
        }

        public static Task FindOne(int id)
        {
            string cStr = ConfigurationManager.ConnectionStrings["TaskManager"].ConnectionString;

            using (SqlConnection cnx = new SqlConnection(cStr))
            {
                string requete = "SELECT * FROM Task WHERE id = " + id;

                using (SqlCommand cmd = new SqlCommand(requete, cnx))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cnx.Open();
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        Task t = new Task();
                        while (dataReader.Read())
                        {
                            t.Id = (int)dataReader["id"];
                            t.Description = (string)dataReader["Description"];
                            t.Client = (string)dataReader["Client"];
                            t.Priority = (int)dataReader["Priority"];
                            t.Creation = (DateTime)dataReader["Creation"];
                            t.Due = (DateTime)dataReader["Due"];
                            if (!dataReader.IsDBNull(dataReader.GetOrdinal("ClosedDate")))
                            {
                                t.ClosedDate = (DateTime)dataReader["ClosedDate"];
                            }
                            t.Done = (Boolean)dataReader["Done"];
                        }
                        return t;
                    }
                }
            }
        }


        public bool SaveAsNew()
        {
            string cN = ConfigurationManager.ConnectionStrings["TaskManager"].ConnectionString;

            using (SqlConnection cnx = new SqlConnection(cN))
            {
                // Utilisation de la connexion
                string requete = "INSERT INTO Task (Description, Client, Priority"
                                +", Creation, Due) VALUES (@Description, @Client"
                                +", @Priority, @Creation, @Due);";

                using (SqlCommand cmd = new SqlCommand(requete, cnx))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    //définir les paramètres
                    cmd.Parameters.Add("Client", SqlDbType.NVarChar);
                    cmd.Parameters.Add("Description", SqlDbType.NVarChar);
                    cmd.Parameters.Add("Priority", SqlDbType.Int);
                    cmd.Parameters.Add("Creation", SqlDbType.DateTime2);
                    cmd.Parameters.Add("Due", SqlDbType.DateTime2);

                    //donner des valeurs aux paramètres
                    cmd.Parameters["Description"].SqlValue = this.Description;
                    cmd.Parameters["Client"].SqlValue = this.Client;
                    cmd.Parameters["Priority"].SqlValue = this.Priority;
                    cmd.Parameters["Creation"].SqlValue = DateTime.Now;
                    cmd.Parameters["Due"].SqlValue = this.Due;

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                    cnx.Close();

                    return true;
                }
            }
        }


        public bool Update()
        {
            string cN = ConfigurationManager.ConnectionStrings["TaskManager"].ConnectionString;

            using (SqlConnection cnx = new SqlConnection(cN))
            {
                // Utilisation de la connexion
                string requete = "UPDATE Task SET ";
                requete += "Description=@Description,";
                requete += "Client=@Client,";
                requete += "Priority=@Priority,";
                requete += "Due=@Due,";
                requete += "ClosedDate=@ClosedDate,";
                requete += "Done=@Done ";
                requete += "WHERE Id = " + this.Id;

                using (SqlCommand cmd = new SqlCommand(requete, cnx))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    //définir les paramètres
                    cmd.Parameters.Add("Description", SqlDbType.NVarChar);
                    cmd.Parameters.Add("Client", SqlDbType.NVarChar);
                    cmd.Parameters.Add("Priority", SqlDbType.Int);
                    cmd.Parameters.Add("ClosedDate", SqlDbType.DateTime);
                    cmd.Parameters.Add("Due", SqlDbType.DateTime);
                    cmd.Parameters.Add("Done", SqlDbType.Bit);

                    //donner des valeurs aux paramètres
                    cmd.Parameters["Description"].SqlValue = this.Description;
                    cmd.Parameters["Client"].SqlValue = this.Client;
                    cmd.Parameters["Priority"].SqlValue = this.Priority;
                    cmd.Parameters["ClosedDate"].SqlValue = (object)this.ClosedDate ?? DBNull.Value;
                    cmd.Parameters["Due"].SqlValue = this.Due;
                    cmd.Parameters["Done"].SqlValue = this.Done;

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                    cnx.Close();

                    return true;
                }
            }
        }

    }
}