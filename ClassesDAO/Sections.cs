//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Exo9
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sections
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sections()
        {
            this.Stagiaire = new HashSet<Stagiaire>();
        }
    
        internal int IdSection { get; set; }
        internal string NomSection { get; set; }
        internal System.DateTime DateDebut { get; set; }
        internal System.DateTime DateFin { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stagiaire> Stagiaire { get; set; }
    }
}
