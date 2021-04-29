//  Copyright (c) 2021 Demerzel Solutions Limited
//  This file is part of the Nethermind library.
// 
//  The Nethermind library is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  The Nethermind library is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public License
//  along with the Nethermind. If not, see <http://www.gnu.org/licenses/>.
// 

using System.Collections.Generic;
using System.Linq;
using Nethermind.Blockchain;
using Nethermind.Blockchain.Processing;
using Nethermind.Blockchain.Producers;
using Nethermind.Consensus;
using Nethermind.Consensus.Transactions;
using Nethermind.Core;
using Nethermind.Int256;
using Nethermind.Logging;

namespace Nethermind.Mev
{
    public class MevBlockProducer : MultipleManualBlockProducer
    {
        public MevBlockProducer(params IManualBlockProducer[] blockProducers) : base(blockProducers)
        {
        }

        protected override BlockProducedContext GetBestBlock(IEnumerable<BlockProducedContext> blocks)
        {
            BlockProducedContext? best = null;
            UInt256 maxBalance = UInt256.Zero;
            foreach (BlockProducedContext context in blocks)
            {
                if (context.ProducedBlock is not null)
                {
                    UInt256 balance = context.PostProducedBlockStateProvider!.GetBalance(context.ProducedBlock.Beneficiary!);
                    if (balance > maxBalance)
                    {
                        best = context;
                        maxBalance = balance;
                    }
                }
            }

            return best ?? new BlockProducedContext(null, null);
        }
    }
}