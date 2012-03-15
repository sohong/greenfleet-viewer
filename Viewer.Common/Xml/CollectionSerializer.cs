////////////////////////////////////////////////////////////////////////////////
// CollectionSerializer.cs
// 2012.03.15, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml.Linq;
using System.IO;

namespace Viewer.Common.Xml {

    /// <summary>
    /// 컬렉션을 xml에 저장하고 읽어들인다.
    /// </summary>
    public class CollectionSerializer {

        #region constructor

        public CollectionSerializer() {
        }

        #endregion // constructor


        #region methods

        public void Serialize(IEnumerable models, string elementName, XContainer parent) {
            new XmlTransformer().Serialize(models, parent, elementName);
        }

        public void Serialize(IEnumerable models, string rootName, string elementName, string fileName) {
            XDocument doc = new XDocument();
            XElement root = new XElement(rootName);
            doc.Add(root);

            Serialize(models, elementName, root);
            Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            doc.Save(fileName);
        }

        public IList Deserialize(XContainer parent, string elementName, Type modelType, IList target) {
            if (target == null) {
                target = new List<object>();
            }
            target = new XmlTransformer().Deserialize(parent, elementName, modelType, target);
            return target;
        }

        public IList Deserialize(string fileName, string elementName, Type modelType, IList target) {
            XDocument doc = XDocument.Load(fileName);
            XElement root = doc.Root;

            target = Deserialize(root, elementName, modelType, target);
            return target;
        }


        #endregion // methods
    }
}
