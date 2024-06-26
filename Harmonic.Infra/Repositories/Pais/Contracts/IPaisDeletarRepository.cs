﻿using Harmonic.Domain.Entities.Pais;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Infra.Repositories.Pais.Contracts;

public interface IPaisDeletarRepository : IDeleteRepository<PaisEntity, int>
{
}
