using LeagueBL.Domein;
using LeagueBL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBL.Interfaces {
    public interface ISpelerRepository {
        Speler SchrijfSpelerInDB(Speler speler);
        bool BestaatSpeler(Speler speler);
        void UpdateSpeler(Speler speler);
        IReadOnlyList<SpelerInfo> SelecteerSpelers(int? id, string naam);
    }
}
