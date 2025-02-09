﻿using FluentAssertions;
using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Infra.Repository;
using HBSIS.Padawan.Produtos.Tests.Builders;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System;

namespace HBSIS.Padawan.Produtos.Tests.Integration.Controllers
{
    public class FornecedorControllerTest : BaseControllerTest
    {
        private readonly FornecedorRepository _fornecedorRepository;

        public FornecedorControllerTest() : base("api/fornecedor")
        {
            _fornecedorRepository = new FornecedorRepository(Factory.GetContext());
        }

        [Fact]
        public async Task Deve_Cadastrar_Um_Novo_Fornecedor()
        {
            var fornecedorRequest = FornecedorHelper.FornecedorGugu;

            var response = await Client.PostAsJsonAsync($"{ControllerUri}/Cadastrar", fornecedorRequest);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var fornecedorList = await _fornecedorRepository.GetAllAsync();

            fornecedorList.Should().HaveCount(1);
            fornecedorList.Should().ContainEquivalentOf(fornecedorRequest);
        }
    }
}
