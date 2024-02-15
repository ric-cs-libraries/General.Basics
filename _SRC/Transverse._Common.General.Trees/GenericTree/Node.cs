﻿using System.Diagnostics;

using Transverse._Common.General.Basics.Extensions;
using Transverse._Common.General.Trees.GenericTree.Interfaces;


namespace Transverse._Common.General.Trees.GenericTree;


[DebuggerDisplay("Node: NbChildren={NbChildren}, Id={Id}, ParentId={ParentId}, DataToString='{Data.ToString()}'")]
public class Node<TData> : TreeElement<TData>
{
    protected readonly List<TreeElement<TData>> elements;


    public int NbChildren => elements.Count;

    public bool HasChild => (elements.Count > 0);


    public TreeElement<TData>? LastAddedElement => (HasChild) ? elements[NbChildren - 1] : null;

    protected Node() : base()
    {
        elements = new List<TreeElement<TData>>();
    }

    public static Node<TData> Create()
    {
        var result = new Node<TData>();
        return result;
    }

    public Node<TData> AddMany(TreeElement<TData>[] elements)
    {
        foreach (var element in elements)
        {
            Add(element);
        }
        return this;
    }

    public Node<TData> Add(TreeElement<TData> element)
    {
        if (element.Id == Id)
        {
            throw new ChildCantBeTheParentException(Id);
        }

        if (OwnsChildById(element))
        {
            throw new ChildAlreadyExistsException(element.Id, Id);
        }

        if (element.HasParent)
        {
            var oldParentNode = (element.Parent! as Node<TData>)!.RemoveChildById(element);
        }

        AddChild(element);
        return this;
    }
    protected virtual void AddChild(TreeElement<TData> element)
    {
        element.Parent = this;
        elements.Add(element);
    }

    public TreeElement<TData> GetChildByIndex(int childIndex)
    {
        elements.CheckIsValidIndex_(childIndex);

        var result = elements.ElementAt(childIndex);
        return result;
    }

    public TreeElement<TData>? GetChildById(int childId)
    {
        var result = elements.FirstOrDefault(te => te.Id == childId);
        return result;
    }

    public bool OwnsChildById(int childId)
    {
        var result = (GetChildById(childId) is not null);
        return result;
    }

    public bool OwnsChildById(TreeElement<TData> childElement)
    {
        var result = OwnsChildById(childElement.Id);
        return result;
    }

    public Node<TData> RemoveChildById(int childId)
    {
        var child = GetChildById(childId);

        if (child is null)
        {
            throw new UnexistingChildException(childId, Id);
        }
        elements.Remove(child);

        return this;
    }

    public Node<TData> RemoveChildById(TreeElement<TData> childElement)
    {
        return RemoveChildById(childElement.Id);
    }


    public override void AcceptVisitor(IVisitor<TData> visitor)
    {
        visitor.Visit(this);
        elements.ForEach(e => e.AcceptVisitor(visitor));
    }

    protected override List<string> GetState()
    {
        var state = base.GetState();

        state.AddRange(new List<string>()
        {
            $"Elements({NbChildren})=[{string.Join(",",elements.Select(te=>te.GetStateAsString()))}]"
        });
        return state;
    }

    public override string GetStateAsString()
    {
        var baseStateAsString = base.GetStateAsString();

        string result = $"{{{baseStateAsString}}}";
        return result;
    }
}
