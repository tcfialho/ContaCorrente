using ContaCorrente.Domain.Commands;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;

namespace ContaCorrente.Tests
{
    public class LancamentoControllerTests : IClassFixture<TestWebApplicationFactory<ContaCorrente.Startup>>
    {
        private static HttpClient _client;
        private readonly TestWebApplicationFactory<ContaCorrente.Startup> _factory;
        private readonly IServiceProvider _serviceProvider;

        public LancamentoControllerTests(TestWebApplicationFactory<ContaCorrente.Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions());
            _serviceProvider = _factory.Server.Host.Services;
        }

        [Fact]
        public async void AoExecutarUmaTransferenciaComSucessoDeveRetornarOK()
        {
            var contaOrigem = Seed.GetContas().First();
            var contaDestino = Seed.GetContas().Last();

            var login = new LancamentoCommand() { ContaOrigem = contaOrigem.Numero, ContaDestino = contaDestino.Numero, Valor = 100 };
            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var message = new HttpRequestMessage(HttpMethod.Post, "/api/lancamento") { Content = content };

            var response = await _client.SendAsync(message);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async void AoExecutarUmaTransferenciaParaUmaContaInexistenteDeveRetornarBadRequest()
        {
            var login = new LancamentoCommand() { ContaOrigem = 0000, ContaDestino = 0000, Valor = 100 };
            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var message = new HttpRequestMessage(HttpMethod.Post, "/api/lancamento") { Content = content };

            var response = await _client.SendAsync(message);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void AoExecutarUmaTransferenciaComValorNegativoDeveRetornarBadRequest()
        {
            var contaOrigem = Seed.GetContas().First();
            var contaDestino = Seed.GetContas().Last();

            var login = new LancamentoCommand() { ContaOrigem = contaOrigem.Numero, ContaDestino = contaDestino.Numero, Valor = -100 };
            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var message = new HttpRequestMessage(HttpMethod.Post, "/api/lancamento") { Content = content };

            var response = await _client.SendAsync(message);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void AoExecutarUmaTransferenciaComOrigemEDestinoIguaisDeveRetornarBadRequest()
        {
            var contaOrigem = Seed.GetContas().First();
            var contaDestino = Seed.GetContas().First();

            var login = new LancamentoCommand() { ContaOrigem = contaOrigem.Numero, ContaDestino = contaDestino.Numero, Valor = 100 };
            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var message = new HttpRequestMessage(HttpMethod.Post, "/api/lancamento") { Content = content };

            var response = await _client.SendAsync(message);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
