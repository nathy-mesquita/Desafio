using ControlePessoas.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ControlePessoas.UnitTests.Core.Entities
{
    public class PessoaTests
    {

        private readonly string[] _cpf = new string[]
        {
            "09519964797","37958496249","51651114749","07426597717","32743858168","43429238900","00040832708",
            "17401129892","04220621970","02034364902","37098404810","86235974604","95249427049","72590025491",
            "09855448723","89338111504","00469076534","21326292854","09844577713","33607516715","75729083653",
            "09889279770","02600235744","30794510906","02535451700","19527061814","61082422720","02416659731",
            "17254462860","07811822717","22146242876","06962797702","10938534769","22211464840","71513418734",
            "25529624839","06560523683","14661804805","22029816833","12760462862","38649519091","12704398712",
            "41705005802","33784061869","33676410653","02419745779","02459205791","02922694470","87162954120"
        };
        private readonly char[] _genero = new char[]
        {
            'F', 'M'
        };
        private readonly DateTime[] _dateTimesMinor = new DateTime[]
        {
            new DateTime(2010, 05, 28),
            new DateTime(2004, 01, 31),
            new DateTime(2005, 11, 05),
            new DateTime(2006, 10, 15),
            new DateTime(2007, 03, 14)
        };
        private readonly DateTime[] _dateTimesMajor = new DateTime[]
        {
            new DateTime(1997, 12, 02),
            new DateTime(1996, 02, 11),
            new DateTime(2002, 02, 12),
            new DateTime(2000, 03, 17),
            new DateTime(1980, 11, 11)
        };
        private readonly List<Pessoa> _pessoaList = new List<Pessoa>()
        {
            new Pessoa("nomeum", "sobrenomeum", "09519964797",  new DateTime(1997, 12, 02), 'F' ),
            new Pessoa("nomedois", "sobrenomedois", "37958496249",   new DateTime(1980, 11, 11), 'M' ),
            new Pessoa("nometres", "sobrenometres", "51651114749",  new DateTime(2002, 02, 12), 'F' ),
            new Pessoa("nomequatro", "sobrenomequatro", "07426597717",  new DateTime(2000, 03, 17), 'M' ),
            new Pessoa("nomecinco", "sobrenomecinco", "86235974604",   new DateTime(1996, 02, 11), 'F' )
        };


        #region Valida CPF 
        [Fact]
        [Trait("Create", "CepValid")]
        public void InputCpf_CreatedPessoa_ReturnCpfValid()
        {
            //Arrange
            var cpf = GetRandomCpf();
            var cpfInvalido = "67812345377";
            var pessoa = GetRandomElement();

            //Act
            var result = IsCpf(pessoa.Cpf);
            if (pessoa.Cpf.Length != 11)
                throw new Exception("CPF deve conter 11 dígitos!");
            if (!result)
                throw new Exception("CPF inválido!");

            //Assert
            Assert.Equal(pessoa.Cpf.Length, 11);
            Assert.True(IsCpf(pessoa.Cpf));
        }
        [Fact]
        [Trait("Create", "PessoaValid")]
        public void InputPessoa_CreatedCpf_ReturnPessoaValid()
        {
            //Não será permitido o cadastro de duas ou mais pessoas com o mesmo CPF
            //Arrange
            var cpf = GetRandomCpf();
            var pessoaExistent = new Pessoa("nometres", "sobrenometres", cpf, new DateTime(2002, 02, 12), 'F');
            var pessoaNova = new Pessoa("nomecinco", "sobrenomecinco", "46323486008", new DateTime(1996, 02, 11), 'F');

            //Act
            var result = IsCpf(pessoaNova.Cpf);
            if (!result)
                throw new Exception("CPF inválido!");
            if (pessoaExistent.Cpf == pessoaNova.Cpf)
                throw new Exception("Cpf já cadastrado!");

            //Assert
            Assert.DoesNotContain(pessoaNova.ToString(), pessoaExistent.Cpf);
            Assert.False(pessoaNova.Cpf == pessoaExistent.Cpf);
        }
        #endregion

        #region Campos Obrigatórios
        [Fact]
        [Trait("Create", "Nome")]
        public void InputPessoa_Created_ReturnNome()
        {
            //Arrange
            var pessoaElement = GetRandomElement();

            //Act
            var nome = pessoaElement.Nome;

            //Assert
            Assert.NotNull(nome);
            Assert.NotEmpty(nome);
            Assert.Contains(nome, pessoaElement.Nome);

        }
        [Fact]
        [Trait("Create", "Sobrenome")]
        public void InputPessoa_Created_ReturSobrenome()
        {
            //Arrange
            var pessoaElement = GetRandomElement();

            //Act
            var sobrenome = pessoaElement.Sobrenome;

            //Assert
            Assert.NotNull(sobrenome);
            Assert.NotEmpty(sobrenome);
            Assert.Contains(sobrenome, pessoaElement.Sobrenome);
        }
        [Fact]
        [Trait("Create", "DataNasciemnto")]
        public void InputEndereco_Created_ReturDataNascimento()
        {
            //Arrange
            var pessoaElement = GetRandomElement();

            //Act
            var dataNascimento = pessoaElement.DataNascimento;
            var dataNascimentoWithString = dataNascimento.ToString();
            var result = pessoaElement.DataNascimento.ToString();

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Contains(dataNascimentoWithString, result);
            Assert.True(pessoaElement.Idade > 18);

        }

        //- Endereços(Obrigatório)
        #endregion

        #region Valida Idade
        [Fact]
        [Trait("CreateDataNasciemnto", "Idade")]
        public void InputDataNascimento_CreatedPessoa_ReturnIdadeValid()
        {
            //Arrange
            var dataInvalida = DateTime.Parse(GetRandomIdadeMinor());
            var pessoaMenor = new Pessoa("nomeum", "sobrenomeum", "09519964797", dataInvalida, 'F');
            var dataValida = DateTime.Parse(GetRandomIdadeMajor());
            var pessoaMaior = new Pessoa("nomedois", "sobrenomedois", "37958496249", dataValida, 'M');
            var pessoa = GetRandomElement();

            //Act
            if (string.IsNullOrEmpty(pessoa.ToString()))
                throw new Exception("Pessoa inválida!");
            if (pessoa.Idade < 18)
                throw new Exception("Não é permitido cadastro de pessoas menores de 18 anos.");

            //Assert
            Assert.NotNull(pessoa);
            Assert.True(pessoa.Idade > 18);
        }
        #endregion

        #region Genero (F / M)
        [Fact]
        [Trait("Create", "Genero")]
        public void InputGenero_CreatedPessoa_ReturnGeneroValid()
        {
            //Arrange
            var genero = GetRandomGenero();
            var pessoa = GetRandomElement();

            //Act
            if (string.IsNullOrEmpty(pessoa.Genero.ToString()) || string.IsNullOrWhiteSpace(pessoa.Genero.ToString()))
                throw new Exception("Genero nulo ou vazio");
            if ((char.Parse(genero) != 'F') && (char.Parse(genero) != 'M'))
                throw new Exception("Genero inválido! Diferente de (F/M)");
            if ((pessoa.Genero != 'F') && (pessoa.Genero != 'M'))
                throw new Exception("Genero inválido! Diferente de (F/M)");

            //Assert
            Assert.NotNull(pessoa.Genero.ToString());
            Assert.NotEmpty(pessoa.Genero.ToString());
            Assert.True((pessoa.Genero == 'F') || (pessoa.Genero == 'M'));
        }
        #endregion


        #region Métodos Auxiliares
        private string GetRandomCpf()
        {
            var random = new Random();
            var start = random.Next(0, _cpf.Length);
            return _cpf[start];
        }
        private string GetRandomIdadeMinor()
        {
            var random = new Random();
            var start = random.Next(0, _dateTimesMinor.Length);
            return _dateTimesMinor[start].ToString();
        }
        private string GetRandomIdadeMajor()
        {
            var random = new Random();
            var start = random.Next(0, _dateTimesMajor.Length);
            return _dateTimesMajor[start].ToString();
        }
        private Pessoa GetRandomElement()
        {
            var lista = _pessoaList;
            var rnd = new Random();
            return lista[rnd.Next(lista.Count)];
        }
        private string GetRandomGenero()
        {
            var random = new Random();
            var start = random.Next(0, _genero.Length);
            return _genero[start].ToString();
        }

        public int CalculaIdade(DateTime dataNascimento)
        {
            int idade = DateTime.Now.Year - dataNascimento.Year;
            if (DateTime.Now.DayOfYear < dataNascimento.DayOfYear)
            {
                idade = idade - 1;
            }
            return idade;
        }

        #region Valida CPF - True CPF válido e False CPF inválido
        private bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
        #endregion
        #endregion
    }
}
