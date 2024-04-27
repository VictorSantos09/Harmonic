﻿using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.TipoConteudo;
using QuickKit.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harmonic.Infra.Repositories.Contracts.TipoConteudo;
public interface ITipoConteudoGetRepository : IGetAllRepository<TipoConteudoEntity>, IGetByIdRepository<TipoConteudoEntity, int>
{

}