using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFrut.Clases
{
    public class Items : IEnumerator, IEnumerable
    {
        private Item[] itemList;
        int position = -1;

        public Items()
        {

        }

        //IEnumerator and IEnumerable require these methods.
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        //IEnumerator
        public bool MoveNext()
        {
            position++;
            return (position < itemList.Length);
        }

        //IEnumerable
        public void Reset()
        { position = 0; }

        //IEnumerable
        public object Current
        {
            get { return itemList[position]; }
        }
    }
}
