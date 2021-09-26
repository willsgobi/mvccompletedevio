using DevIO.Business.Models;
using System;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IProdutoService
    {
        Task Adicionar(Produto fornecedor);
        Task Atualizar(Produto fornecedor);
        Task Remover(Guid id);
    }
}
