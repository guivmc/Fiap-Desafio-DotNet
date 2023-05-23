namespace FiapTestAPI.Models
{
    public class TurmaModel
    {
        public int ID { get; set; }
        public int CursoID { get; set; }
        public string Turma { get; set; } = null!;
        public int Ano { get; set; }
    }
}
