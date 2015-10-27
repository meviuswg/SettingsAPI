using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SettingsAPIRepository.Util
{
    internal class TransactionScopeFactory
    {
        public static TransactionScope CreateReaduncommited()
        {
            return new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted });
        }
    }
}
