﻿using General.Basics.Generators;
using General.Basics.Reflection.Extensions;
using General.Basics.Trees.GenericTree.Interfaces;


namespace General.Basics.Trees.GenericTree.Abstracts;

public abstract class TreeElement<TData>
{
    public TData? Data { get; set; }


    public int Id { get; }

    public int? ParentId => Parent?.Id;
    public virtual TreeElement<TData>? Parent { get; protected internal set; }
    public bool HasParent => (Parent is not null);

    public virtual int? IndexInParent { get; protected internal set; } = null;


    private const int INITIAL_ID = 0;
    private static IdsGenerator idsGenerator = IdsGenerator.Create(INITIAL_ID, intIdStep: 1);


    public abstract void AcceptVisitor(IVisitor<TData> visitor);

    protected TreeElement()
    {
        Id = idsGenerator.GetNextIntId();
    }

    public static void ResetId()
    {
        idsGenerator.ResetIntId();
    }

    protected internal void LinkToParent(TreeElement<TData> parent, int indexInParent)
    {
        Parent = parent;
        IndexInParent = indexInParent;
    }
    protected internal void UnlinkParent()
    {
        Parent = null;
        IndexInParent = null;
    }

    protected virtual List<string> GetState()
    {
        List<string> state = new()
        {
            $"Id={Id}",
            $"ParentId={ParentId}",
            $"Data='{Data?.ToString()}'"
        };
        return state;
    }

    public virtual string GetStateAsString()
    {
        List<string> state = GetState();

        string result = $"{GetType().GetName_()}: " + string.Join("; ", state);
        return result;
    }
}