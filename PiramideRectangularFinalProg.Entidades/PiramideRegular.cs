using System.Text;

namespace PiramideRectangularFinalProg.Entidades
{
    public class PiramideRegular
    {
        public double LadoBase { get;  set; }
        public double Altura { get;  set; }
        public Material Material { get; set; }
        public PiramideRegular()
        {
            
        }
        public PiramideRegular(double ladoBase, double altura, Material material)
        {
            if (ladoBase <= 0)
                throw new ArgumentException("El lado de la base debe ser mayor a cero.");
            if (altura <= 0)
                throw new ArgumentException("La altura debe ser mayor a cero.");

            LadoBase = ladoBase;
            Altura = altura;
            Material = material;
        }

        private double AreaLateral()
        {
            return LadoBase * Altura / 2;

        }
        public double GetVolumen()
        {
            return AreaBase() * Altura / 3;
        }

        private double AreaBase()
        {
            return Math.Pow(LadoBase, 2);
        }

        public double GetArea()
        {
            return AreaBase() + AreaLateral() * 4;
        }

        public string InformarDatos()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Pirámide Regular");
            sb.AppendLine($"Lado de la Base: {LadoBase}");
            sb.AppendLine($"Altura: {Altura}");
            sb.AppendLine($"Material: {Material}");
            return sb.ToString();
        }
    }
}