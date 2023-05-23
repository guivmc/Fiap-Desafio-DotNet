using System.ComponentModel;

namespace FiapTestWeb.Models
{
    public class TurmaModel
    {
        public int ID { get; set; }
        [DisplayName("Curso")]
        public int CursoID { get; set; }
        public string Turma { get; set; } = null!;
        public int Ano { get; set; }
    }
}
