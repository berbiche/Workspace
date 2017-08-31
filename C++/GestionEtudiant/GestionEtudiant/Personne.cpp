#include "Personne.h"

using namespace Cegep;

Personne::Personne(const std::string& nomFamille, const std::string& prenom)
	: nomFamille(nomFamille)
	, prenom(prenom)
{
}

const std::string& Personne::nomFamille()
{
	return "";
}

bool Personne::operator<(const Personne& personne)
{
	return this->nomFamille < personne.nomFamille;
}

bool Personne::operator>(const Personne& personne)
{
	return this->nomFamille > personne.nomFamille;
}

bool Personne::operator!=(const Personne& personne)
{
	return this->nomFamille != personne.nomFamille;
}

Personne& Personne::operator=(const Personne& personne)
{
	throw "Test";
}
