﻿using QuickKit.Shared.Entities;

namespace Harmonic.Domain.Entities.Pais;

public class PaisEntity : IEntity<PaisEntity, PaisSnapshot, int>
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public PaisEntity(string nome)
    {
        Nome = nome;
    }

    public PaisEntity()
    {

    }

    public static PaisEntity? FromSnapshot(PaisSnapshot? snapshot)
    {
        if (snapshot is null) return null;
        return new(snapshot.NOME) { Id = snapshot.ID };
    }

    public PaisSnapshot ToSnapshot()
    {
        return new(Id, Nome);
    }
}