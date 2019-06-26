/*
 * Copyright (c) 2018 Demerzel Solutions Limited
 * This file is part of the Nethermind library.
 *
 * The Nethermind library is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * The Nethermind library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with the Nethermind. If not, see <http://www.gnu.org/licenses/>.
 */

namespace Nethermind.Cli.Modules
{
    [CliModule("trace")]
    public class ParityCliModule : CliModuleBase
    {
        [CliFunction("trace", "replayTransaction")]
        public object ReplayTransaction(string txHash, string[] traceTypes)
        {
            return NodeManager.Post<object>("trace_replayTransaction", txHash, traceTypes).Result;
        }
        
        [CliFunction("trace", "replayBlockTransactions")]
        public object ReplayBlockTransactions(string blockNumber, string[] traceTypes)
        {
            return NodeManager.Post<object>("trace_replayBlockTransactions", blockNumber, traceTypes).Result;
        }

        public ParityCliModule(ICliEngine cliEngine, INodeManager nodeManager) : base(cliEngine, nodeManager)
        {
        }
    }
}