using System;

namespace VeriErisimKatmani
{
    public class Kategori
    {
        public int ID { get; set; }
        public string Isim { get; set; }
        public string Aciklama { get; set; }
        public bool Durum { get; set; }
        public bool Silinmis { get; set; }

        // ToString metodunu burada tanımlayın
        public override string ToString()
        {
            return Isim; // Kategori ismini döndürüyor
        }
    }
}
