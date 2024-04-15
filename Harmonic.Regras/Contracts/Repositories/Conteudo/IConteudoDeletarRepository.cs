﻿using Harmonic.Domain.Entities.Conteudo;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Regras.Contracts.Repositories.Conteudo;

public interface IConteudoDeletarRepository : IDeleteRepository<ConteudoEntity, int> { }