using ControlePessoas.Core.Entities;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ControlePessoas.UnitTests.Core.Entities
{
    public class EnderecoTests
    {

        private const string APICEP = "https://brasilapi.com.br/api/cep/v1/";

        private readonly string[] _logradouro = new string[]
        {
            "Rua 1", "Rua 2", "Rua 3","Rua 4", "Rua 5"
        };
        private readonly string[] _complemento = new string[]
        {
            "complemento 1", "complemento 2", "complemento 3","complemento 4", "complemento 5"
        };
        private readonly string[] _bairro = new string[]
        {
            "bairro 1", "bairro 2", "bairro 3","bairro 4", "bairro 5"
        };
        private readonly string[] _cidade = new string[]
        {
            "cidade 1", "cidade 2", "cidade 3","cidade 4", "cidade 5"
        };
        private readonly string[] _estado = new string[]
        {
            "estado 1", "estado 2", "estado 3","estado 4", "estado 5"
        };
        private readonly string[] _cep = new string[]
        {
            "20211290", "20251030", "48080120", "69928970", "78480970"
        };
        private readonly List<Endereco> _enderecoList = new List<Endereco>()
        {
           new Endereco(TipoEnderecoEnum.Comercial, "Rua 1", 1, "complemento 1", "bairro 1", "cidade 1", "estado 1", "20211290", 1),
           new Endereco(TipoEnderecoEnum.Comercial, "Rua 2", 1, "complemento 2", "bairro 2", "cidade 2", "estado 2", "20211290", 1),
           new Endereco(TipoEnderecoEnum.Comercial, "Rua 3", 1, "complemento 3", "bairro 3", "cidade 3", "estado 3", "20211290", 1),
           new Endereco(TipoEnderecoEnum.Comercial, "Rua 4", 1, "complemento 4", "bairro 4", "cidade 4", "estado 4", "20211290", 1),
           new Endereco(TipoEnderecoEnum.Comercial, "Rua 5", 1, "complemento 5", "bairro 5", "cidade 5", "estado 5", "20211290", 1)
        };


        #region TipoEndereco
        [Theory]
        [InlineData(TipoEnderecoEnum.Cobranca)]
        [InlineData(TipoEnderecoEnum.Comercial)]
        [InlineData(TipoEnderecoEnum.Residencial)]
        public void InputEndereco_Created_ReturnTipoEnderecoEnumIsValid(TipoEnderecoEnum value)
        {
            //Arrange
            var endereco = new Endereco(value, "Rua Itapiru", 33, "casa", "Catumbi", "Rio de janeiro", "RJ", "20211290", 1);

            //Act
            var result = endereco.Tipo;
            Assert.NotEmpty(endereco.Cep);
            Assert.NotEmpty(endereco.Numero.ToString());

            //Assert
            Assert.Equal(result, value);
        }
        #endregion

        #region Cep
        [Fact]
        [Trait("Create", "Cep")]
        public void InputCep_CreatedEndereco_ReturnCepValid()
        {

            //Arrange
            string cepRandom = GetRandomCep();
            var cep = new CepViewModel();
            var client = new RestClient(APICEP);
            var request = new RestRequest(cepRandom, Method.GET);
            var response = client.Execute<CepViewModel>(request);
            cep = response.Data;

            //Act
            if (string.IsNullOrEmpty(cep.Cep))
                throw new Exception("Cep inválido!");

            if (cep.ToString().Length < 8)
                throw new Exception("Cep inválido! Com menos de 8 digitos.");

            //Assert 
            Assert.NotEmpty(cep.Cep);
            Assert.Equal(8, cep.Cep.Length);
            Assert.IsType<CepViewModel>(cep);

        }
        #endregion

        #region Obrigatoriedade dos Campos
        [Fact]
        [Trait("Create", "Logradouro")]
        public void InputEndereco_Created_ReturnLogradouroNotNull()
        {
            //Arrange
            var elementList = GetRandomElement();

            //Act
            var logradouro = elementList.Logradouro;

            //Assert
            Assert.NotNull(logradouro);
        }

        [Theory]
        [InlineData(1)]
        [Trait("Create", "Numero")]
        public void InputEndereco_Created_ReturnNumeroNotNull(int value)
        {
            //Arrange
            var elementList = GetRandomElement();

            //Act
            var numero = elementList.Numero;

            //Assert
            Assert.NotNull(numero.ToString());
            Assert.NotEmpty(numero.ToString());
            Assert.Equal(value, numero);
        }

        [Fact]
        [Trait("Create", "Bairro")]
        public void InputCep_Created_ReturnBairroValid()
        {
            //Arrange
            string cepRandom = GetRandomCep();
            var cep = new CepViewModel();
            var client = new RestClient(APICEP);
            var request = new RestRequest(cepRandom, Method.GET);
            var response = client.Execute<CepViewModel>(request);
            cep = response.Data;

            var elementList = GetRandomElement();
            var bairro = elementList.Bairro;

            //Act
            if (string.IsNullOrEmpty(cep.Neighborhood))
                throw new Exception("Bairro inválido!");

            bairro = cep.Neighborhood;

            //Assert

            Assert.Contains(bairro, cep.Neighborhood);
        }

        [Fact]
        [Trait("Create", "Cidade")]
        public void InputCep_Created_ReturnCidadeValid()
        {
            //Arrange
            string cepRandom = GetRandomCep();
            var cep = new CepViewModel();
            var client = new RestClient(APICEP);
            var request = new RestRequest(cepRandom, Method.GET);
            var response = client.Execute<CepViewModel>(request);
            cep = response.Data;

            var elementList = GetRandomElement();
            var cidade = elementList.Cidade;

            //Act
            if (string.IsNullOrEmpty(cep.City))
                throw new Exception("Cidade inválida!");

            cidade = cep.City;

            //Assert

            Assert.Contains(cidade, cep.City);
        }

        [Fact]
        [Trait("Create", "Estado")]
        public void InputCep_Created_ReturnEstadoValid()
        {
            //Arrange
            string cepRandom = GetRandomCep();
            var cep = new CepViewModel();
            var client = new RestClient(APICEP);
            var request = new RestRequest(cepRandom, Method.GET);
            var response = client.Execute<CepViewModel>(request);
            cep = response.Data;

            var elementList = GetRandomElement();
            var estado = elementList.Estado;

            //Act
            if (string.IsNullOrEmpty(cep.State))
                throw new Exception("Estado inválido!");
            if (cep.State.Length > 2)
                throw new Exception("Estado com mais de dois digitos.");

            estado = cep.State;

            //Assert
            Assert.Equal(estado, cep.State);
            Assert.Equal(2, estado.Length);
        }
        #endregion

        #region Duplicidade de Endereço - APPLICATION
        [Fact]
        [Trait("Create", "Endereco")]
        public void InputEnderecoSameTipoAndSamePessoa_Created_ReturnEnderecoInvalid()
        {
            //Arrange
            var enderecoExitente = new Endereco(TipoEnderecoEnum.Comercial, "Rua 1", 1, "complemento 1", "bairro 1", "cidade 1", "estado 1", "20211290", 1);
            var enderecoNovoInvalido = new Endereco(TipoEnderecoEnum.Comercial, "Rua 2", 1, "complemento 2", "bairro 2", "cidade 2", "estado 2", "20211290", 1);
            var enderecoNovo = new Endereco(TipoEnderecoEnum.Residencial, "Rua 2", 1, "complemento 2", "bairro 2", "cidade 2", "estado 2", "20211290", 2);
            //Act
            if ((enderecoExitente.Tipo == enderecoNovo.Tipo) && (enderecoExitente.IdPessoa == enderecoNovo.IdPessoa))
                throw new Exception("Endereço de tipos iguais para pessoas iguais! Não é possível cadastrar.");


            //Assert
            Assert.True(enderecoExitente.Tipo != enderecoNovo.Tipo);
            Assert.True(enderecoExitente.IdPessoa != enderecoNovo.IdPessoa);
        }
        #endregion


        #region CepViewModel
        public class CepViewModel
        {
            [JsonProperty("cep")]
            public string Cep { get; set; }
            [JsonProperty("state")]
            public string State { get; set; }
            [JsonProperty("city")]
            public string City { get; set; }
            [JsonProperty("neighborhood")]
            public string Neighborhood { get; set; }
            [JsonProperty("street")]
            public string Street { get; set; }
            [JsonProperty("service")]
            public string Service { get; set; }
        }
        #endregion

        #region Métodos Auxiliares


        private string GetRandomCep()
        {
            var random = new Random();
            var start = random.Next(0, _cep.Length);
            return _cep[start];
        }
        private string GetRandomLogradouro()
        {
            var random = new Random();
            var start = random.Next(0, _logradouro.Length);
            return _logradouro[start];
        }
        private string GetRandomComplemento()
        {
            var random = new Random();
            var start = random.Next(0, _complemento.Length);
            return _complemento[start];
        }
        private string GetRandomBairro()
        {
            var random = new Random();
            var start = random.Next(0, _bairro.Length);
            return _bairro[start];
        }
        private string GetRandomCidade()
        {
            var random = new Random();
            var start = random.Next(0, _cidade.Length);
            return _cidade[start];
        }
        private string GetRandomEstado()
        {
            var random = new Random();
            var start = random.Next(0, _estado.Length);
            return _estado[start];
        }
        private Endereco GetRandomElement()
        {
            var lista = _enderecoList;
            var rnd = new Random();
            return lista[rnd.Next(lista.Count)];
        }


        #endregion
    }
}
