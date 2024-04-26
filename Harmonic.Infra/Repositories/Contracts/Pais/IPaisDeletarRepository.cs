﻿using Harmonic.Domain.Entities.Pais;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Infra.Repositories.Contracts.Pais;

public interface IPaisDeletarRepository : IDeleteRepository<PaisEntity, int>
{
}
