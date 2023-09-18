namespace Projeto.Loja.Entities
{
    public class BaseAudityEntity
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }

        public BaseAudityEntity() {
            if (Id == 0) {
                DataCriacao = DateTime.Now;
                Ativo = true;
            }
            else
                DataAtualizacao = DateTime.Now;
        }

    }
}
