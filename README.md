# Projeto M - Clean Architecture

### Descrição
        * Projeto inicialmente criado e desenvolvido seguindo o artigo: https://juldhais.net/clean-architecture-in-asp-net-core-web-api-4e5ef0b96f99
        * Este projeto foi feito para testar a implementação de inúmeras ferramentas/tecnologias seguindo o padrão de design da arquitetura limpa e do clean code.

### Organização do Projeto
        - Camada de apresentação
                . CleanArchitecture.WebApi

        - Camada de Aplicação
                . CleanArchitecture.Application

        - Camada de Infraestrutura
                . CleanArchitecture.Infraestructure.Communication
                . CleanArchitecture.Infraestructure.Persistence
                . CleanArchitecture.Infraestructure.Security

        - Camada de Domínio
                . CleanArchitecture.Domain

        - Camda de Testes
                . CleanArchitecture.UnitTest


### Implementações
        - Autenticação e autorização (To Do)
        - CQRS (Done)
        - Envio de e-mail (Done)
        - Log (Done)
        - Relacionamento de tabelas (To Do)
        - Segurança - Hash de senhas (Done)
        - Testes unitários (To Do)
        - Tratamento de erros (Done)

### Autenticação e autorização
        - A fazer...

### CQRS
        - Não estou utilizando o MediatR como intermediador. Basicamente possuo 3 classes centrais:
                . BaseRequest
                . BaseResponse
                . IHandler<BaseRequest, BaseResponse>
        - O padrão acima deve ser utilizado para quaisquer fluxos adotados na aplicação

### Envio de e-mail (Done)
        - O envio de e-mail está sendo realizado através do provedor https://ethereal.email/login
        - Ele cria uma conta temporária para você poder testar os envios de e-mail, basta anotar as credenciais
        geradas e as substituir corretamente no "appsettings.json".

### Log
        - Estou utilizando o Serilog com a opção de exibir as informações de erros relevantes no console e criar um
        arquivo ".txt" de log caso algum erro não mapeado apareça.

### Relacionamento de tabelas
        - A fazer...

### Segurança - Hashes de senha
        - Com relação a geração de senhas e manipulação de usuário, não estou utilizando o ASPNET Identity.
        - O algoritmo de hasheamento de senhas e geração de Salt utilizado é o Argon2.

### Testes unitários
        - Particularmente utilizo o MSTest, ele é simples e atende bem ao que preciso.
        - Nos testes unitários devem ser coberto:
                . Inicialização das entidades e dos Value Objects
                . Métodos dos Services criados
                . Handlers e seu fluxo
                . Queries da classe estática
        - Fuja de quaisquer testes que envolvam:
                . Manipulação de arquivos externos
                . Acesso a banco de dados
                . Envio/solicitação de recursos externos
                . CRUD

### Tratamento de erros
        - Estou utilizando os FilterException para centralizar as exceções personalizadas geradas nas camadas mais abaixo,
        na camada de apresentação. É possível obter mais detalhes no link: https://medium.com/swlh/clean-architecture-best-exception-handling-with-consistent-responses-in-asp-net-core-api-b22b07a08e38
        - Em suma, a ideia do FilterException é você levantar uma exceção personalizada em algum ponto da sua aplicação e,
        através do dicionário que mapeia a exceção levantada com seu respectivo método de resolução (Handler da exceção), 
        tratar ela numa camada superior de forma centralizada.