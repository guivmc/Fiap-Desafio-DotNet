using System.ComponentModel;

namespace FiapTestWeb.Models
{
    public class RelacaoModel
    {
        [DisplayName("ID")]
        public int AlunoID { get; set; }
        [DisplayName("ID")]
        public int TurmaID { get; set; }
        [DisplayName("Aluno")]
        public string? AlunoNome { get; set; }
        [DisplayName("Turma")]
        public string? TurmaNome { get; set; }

        public List<AlunoModel>? Alunos { get; set; }
        public List<TurmaModel>? Turmas { get; set; }
    }
}
