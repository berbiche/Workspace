#ifndef ETUDIANT_H
#define ETUDIANT_H

#include <string>
#include "Personne.h"

namespace Cegep
{

class Etudiant : public Personne {
public:
	Etudiant(const std::string& nomFamille,
			 const std::string& prenom,
			 const std::string& noDossier);

private:
};

}

#endif // !ETUDIANT_H
