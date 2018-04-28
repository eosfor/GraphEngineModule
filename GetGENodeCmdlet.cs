using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Management.Automation;
using Trinity;
using Trinity.Storage.Composite;
using System.Reflection;
using Trinity.Utilities;
using Trinity.Storage;
using System.Linq;
using Newtonsoft.Json;

namespace GraphEngineModule
{
    [Cmdlet("Get", "GEVertex")]
    [Alias("Get-Vertex")]
    public class GetGEVertexCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "ByID")]
        public long NodeID;

        [Parameter(Mandatory = true, ParameterSetName = "ByTypeName")]
        public string TypeName;

        protected override void BeginProcessing()
        {
            if (!GlobalState.Instance.IsInitialized)
            {
                Global.Initialize();
            }
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            IEnumerable<ICell> ret;
            switch (ParameterSetName)
            {
                case "ByID":
                    ByID();
                    break;
                case "ByTypeName" :
                    ByTypeName();
                    break;
                default:
                    break;
            }

            
            //base.ProcessRecord();
        }

        private void ByID()
        {

            IEnumerable<ICell> result = from node in Global.LocalStorage.GenericCell_Selector()
                                        where node.CellId == NodeID
                                        select node;
            WriteObject(result, true);
            //using  ( var node = Global.LocalStorage.UseGenericCell(NodeID))
            //{
            //    var o = ConvertToPSObject(node);
            //    WriteObject(o);

            //}           
        }

        // this is to decouple references to objects stored in the Storage from ones returned to the user. ???
        private static PSObject ConvertToPSObject(ICell node)
        {
            var fields = node.GetFieldNames();
            PSObject o = new PSObject();

            foreach (var item in fields)
            {
                var propValue = node.GetField<string>(item);
                var p = new PSNoteProperty(item, propValue.ToString());
                o.Properties.Add(p);
            }
            
            return o;
        }

        private void ByTypeName ()
        {
            IEnumerable<ICell> result = from node in Global.LocalStorage.GenericCell_Selector()
                                        where node.TypeName == TypeName
                                        select node;


            WriteObject(result,true);
        }
    }
}
