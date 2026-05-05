namespace OSKanban.Models
{
    public class OrdemServico
    {
        public int Id { get; set; }
        public string Unidade { get; set; }
        public string Tecnico { get; set; }
        public string Status { get; set; }
        public DateTime Data { get; set; }
        public string Patrimonio { get; set; }
        public string Defeito { get; set; }
        public string Observacoes { get; set; }
    }
}