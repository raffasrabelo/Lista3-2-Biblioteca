using System;
class Program
{
    struct Livros
    {
        public string titulo;
        public string autor;
        public int ano;
        public int prateleira;
    }

    static void cadastraLivro(List<Livros> lista)
    {
        Livros livros = new Livros(); // padrão para instanciar uma struct
        Console.WriteLine("Digite o título do livro: ");
        livros.titulo = Console.ReadLine();
        Console.WriteLine("Digite o nome do autor: ");
        livros.autor = Console.ReadLine();
        Console.WriteLine("Digite o ano de lançamento: ");
        livros.ano = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Digite o número da prateleira onde se localiza: ");
        livros.prateleira = Convert.ToInt32(Console.ReadLine());
        lista.Add(livros);
    }

    static void procuraLivro(List<Livros> lista, string livroBuscar)
    {
        int quantidade = lista.Count();
        for (int i = 0; i < quantidade; i++)
        {
            if (lista[i].titulo.ToUpper().Equals(livroBuscar.ToUpper())) // to upper nos dois dados a serem comparados
            {
                Console.WriteLine($"O livro {lista[i].titulo} está localizado na prateleira {lista[i].prateleira}.");
            }
        }
    }

    static void listarLivros(List<Livros> lista)
    {
        int quantidade = lista.Count();
        for (int i = 0; i < quantidade; i++)
        {
            Console.WriteLine("***Dados do Livro***");
            Console.WriteLine("Título: " + lista[i].titulo);
            Console.WriteLine("Autor: " + lista[i].autor);
            Console.WriteLine("Ano: " + lista[i].ano);
            Console.WriteLine("Prateleira: " + lista[i].prateleira);
            Console.WriteLine("---------------------");
        }
    }

    static void filtraPorAno(List<Livros> lista, int anoBusca)
    {
        int quantidade = lista.Count();
        for (int i = 0; i < quantidade; i++)
        {
            if (lista[i].ano > anoBusca)
            {
                Console.WriteLine("***Dados do Livro***");
                Console.WriteLine("Título: " + lista[i].titulo);
                Console.WriteLine("Autor: " + lista[i].autor);
                Console.WriteLine("Ano: " + lista[i].ano);
                Console.WriteLine("Prateleira: " + lista[i].prateleira);
                Console.WriteLine("---------------------");
            }
        }
    }

    static int menu()
    {
        Console.WriteLine("***Sistema de cadastro de Livros***");
        Console.WriteLine("1- Cadastrar livro;");
        Console.WriteLine("2- Buscar livro pelo nome;");
        Console.WriteLine("3- Listar livros;");
        Console.WriteLine("4- Filtrar livros por ano (ano de lançamento > ano buscado);");
        Console.WriteLine("0- Sair;");
        int opc = Convert.ToInt32(Console.ReadLine());
        return opc;
    }

    static void salvarDados(List<Livros> livros, string nomeArquivo)
    {

        using (StreamWriter writer = new StreamWriter(nomeArquivo))
        {
            foreach (Livros livro in livros )
            {
                writer.WriteLine($"{livro.titulo},{livro.autor},{livro.ano},{livro.prateleira}");
            }
        }
        Console.WriteLine("Dados salvos com sucesso!");


    }

    static void carregarDados(List<Livros> livros, string nomeArquivo)
    {
        if (File.Exists(nomeArquivo))
        {
            string[] linhas = File.ReadAllLines(nomeArquivo);
            foreach (string linha in linhas)
            {
                string[] campos = linha.Split(',');
                Livros livro = new Livros
                {
                    titulo = campos[0],
                    autor = campos[1],
                    ano = int.Parse(campos[2]),
                    prateleira = int.Parse(campos[3])
                };
                livros.Add(livro);
            }
            Console.WriteLine("Dados carregados com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo não encontrado :(");
        }
    }
    static void Main()
    {
        List<Livros> listaDeLivros = new List<Livros>(); // padrão para instanciar uma lista
        int op;
        carregarDados(listaDeLivros, "dadosLivros.txt");
        do
        {
            op = menu();
            switch (op)
            {
                case 1:
                    cadastraLivro(listaDeLivros);
                    break;
                case 2:
                    Console.WriteLine("Digite o nome do livro que deseja procurar: ");
                    procuraLivro(listaDeLivros, Console.ReadLine());
                    break;
                case 3:
                    listarLivros(listaDeLivros);
                    break;
                case 4:
                    Console.WriteLine("Digite o ano a partir do qual você deseja encontrar livros: ");
                    int ano = Convert.ToInt32(Console.ReadLine());
                    filtraPorAno(listaDeLivros, ano);
                    break;
                case 0:
                    Console.WriteLine("Saindo...");
                    salvarDados(listaDeLivros, "dadosLivros.txt");
                    break;

            }// fim switch
            Console.ReadKey();// pausa
            Console.Clear(); // limpa
        } while (op != 0);

        Console.ReadLine();// pausa antes de fechar o programa
    }
}