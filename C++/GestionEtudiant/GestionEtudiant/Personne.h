#ifndef PERSONNE_H
#define PERSONNE_H

#include <string>

namespace Cegep
{

class Personne {
public:
	const std::string& nomFamille();

	virtual bool operator<(const Personne&);
	virtual bool operator>(const Personne&);
	virtual bool operator!=(const Personne&);
	virtual Personne& operator=(const Personne&);
	//virtual ostream& operator <<()

protected:
	Personne(const std::string& nom, const std::string& prenom);
	std::string nomFamille;
	std::string prenom;
	
};

}

#endif // !PERSONNE_H
