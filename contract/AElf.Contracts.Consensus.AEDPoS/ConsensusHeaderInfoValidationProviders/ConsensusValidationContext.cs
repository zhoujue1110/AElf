using System.Collections.Generic;
using AElf.Sdk.CSharp.State;

namespace AElf.Contracts.Consensus.AEDPoS
{
    public class ConsensusValidationContext
    {
        public long CurrentTermNumber { get; set; }
        public long CurrentRoundNumber { get; set; }

        /// <summary>
        /// We can trust this because we already validated the pubkey
        /// during `AEDPoSExtraDataExtractor.ExtractConsensusExtraData`
        /// </summary>
        public string Pubkey => ExtraData.SenderPubkey.ToHex();

        public Round BaseRound { get; set; }

        /// <summary>
        /// This validation focuses on the new round information.
        /// </summary>
        public Round ProvidedRound
        {
            get
            {
                switch (ExtraData.Behaviour)
                {
                    case AElfConsensusBehaviour.UpdateValue:
                        return BaseRound.RecoverFromUpdateValue(ExtraData.Round, Pubkey);
                    case AElfConsensusBehaviour.TinyBlock:
                        return BaseRound.RecoverFromTinyBlock(ExtraData.Round, Pubkey);
                    default:
                        return ExtraData.Round;
                }
            }
        }

        public Dictionary<long, Round> RoundsDict { get; set; }
        public MappedState<long, Round> Rounds { get; set; }
        public LatestProviderToTinyBlocksCount LatestProviderToTinyBlocksCount { get; set; }
        public AElfConsensusHeaderInformation ExtraData { get; set; }
    }
}