﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BriefFiniteElementNet
{
    /// <summary>
    /// Represents an abstract class for being used by Node and Element classes
    /// </summary>
    [DebuggerDisplay("Label: {Label}")]
    public abstract class StructurePart:ISerializable
    {
        /// <summary>
        /// Represents the hash code of ID, used for better performance (probably!)
        /// Anytime id is changed, it should change accordingly to hash code of new value
        /// </summary>
        protected int idHashCode;
        protected Guid id;
        protected string label;
        protected string tag;
        protected Model parent;



        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id
        {
            get { return id; }
        }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        /// <exception cref="BriefFiniteElementNet.InvalidLabelException">member with same label exist in the structure</exception>
        /// <exception cref="System.NotImplementedException"></exception>
        public string Label
        {
            get { return label; }
            set
            {
                if(value!=null)
                    if(parent!=null)
                        if (!parent.IsValidLabel(value))
                            throw new InvalidLabelException(string.Format("member with same label ({0}) exist in the structure",value));

                this.label = value;
            }
        }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>
        /// The tag.
        /// </value>
        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        internal Model Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("idHashCode", idHashCode);
            info.AddValue("id", id);
            info.AddValue("label", label);
            info.AddValue("tag", tag);
        }

        /// <summary>
        /// This is constructor for deserialization. Satisfies the rule CA2229.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The source (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
        protected StructurePart(SerializationInfo info, StreamingContext context)
        {
            idHashCode = info.GetValue<int>("idHashCode");
            id = info.GetValue<Guid>("id");
            label = info.GetValue<string>("label");
            tag = info.GetValue<string>("tag");
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="StructurePart"/> class.
        /// </summary>
        protected StructurePart()
        {
        }
    }
}