using Exo9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Exo9
{

    /// <summary> 
    /// /// classe accès aux données - BDD : Formations, table : Stagiaires 
    /// /// </summary>
    public static class MStagiaireDAOEFStatic
    {
        public static void InstancieStagiairesSection(Sections laSection)
        {
            // instancier le dbContext au besoin (ie. la base de donnee)
            if(DonnesDAO.DbContextFormation == null)
            {
                DonnesDAO.DbContextFormation = new FormationContainer();
                // requête Linq pour lire la BDD (génère les requ. SQL)

                var query = from a in DonnesDAO.DbContextFormation.StagiaireSet
                            where a.Sections.IdSection == laSection.IdSection
                            select a;



                // ref d'objet générique (pour la collection)
                MStagiaire leStagiaire;

            
                // parcourt les lignes de la requête
                foreach(Stagiaire item in query)
                {
                    // instancie et renseigne l'objet MStagiaire spécialisé
                    if(item is StagiaireCIF)
                    {
                        leStagiaire = new MStagiaireCIF(item.NumOsia, item.NomStagiaire, item.PrenomStagiaire, item.rueStagiaire, item.VilleStagiaire, item.CodePostalStagiaire, 
                            ((StagiaireCIF)item).Fongecif.Trim(), ((StagiaireCIF)item).TypeCIF.Trim());
                    } 
                    else
                    {
                        // cas d'un DE : objet MStagiaireDE
                        leStagiaire = new MStagiaireDE(item.NumOsia, item.NomStagiaire, item.PrenomStagiaire, item.rueStagiaire, item.VilleStagiaire, item.CodePostalStagiaire,
                            ((StagiaireDE)item).RemuAfpa);
                    }


                    // affecter points et notes 
                    // on ne peut affecter directement pointsnotes et nbreNotes 
                    // dans MStagiaire que si le demandeur est de ce type MStagiaireDAOEFStatic

                    leStagiaire.SetPoints(Convert.ToDouble(item.pointsNote), (int)item.NbreNotes, typeof(MStagiaireDAOEFStatic).ToString());

                    // ajoute le Stagiaire à la collection de la section
                    //laSection.Ajopu(leStagiaire);


                }




            }




        }






    }
}
