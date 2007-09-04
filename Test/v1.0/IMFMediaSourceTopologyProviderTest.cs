// http://msdn2.microsoft.com/en-us/library/ms697285.aspx

using System;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

using MediaFoundation;
using MediaFoundation.Misc;

namespace Testv10
{
    class IMFMediaSourceTopologyProviderTest
    {
        public void DoTests()
        {
            Again();
        }

        private void Again()
        {
            IMFSourceResolver sr;
            MFObjectType pObjectType;
            object pSource;
            IMFMediaSession pMediaSession;
            IMFMediaSource ms;
            IMFTopology pt;

            MFExtern.MFCreateMediaSession(null, out pMediaSession);

            IMFSequencerSource pSequencerSource;
            MFExtern.MFCreateSequencerSource(null, out pSequencerSource);

            MFExtern.MFCreateSourceResolver(out sr);

            sr.CreateObjectFromURL(
                @"file://c:/sourceforge/mflib/test/media/AspectRatio4x3.wmv",
                MFResolution.MediaSource,
                null,
                out pObjectType,
                out pSource);

            ms = pSource as IMFMediaSource;

            MFExtern.MFCreateTopology(out pt);

            // http://msdn2.microsoft.com/en-us/library/ms701605.aspx

            int sid;
            pSequencerSource.AppendTopology(pt, MFSequencerTopologyFlags.Last, out sid);

            SetFirstTopology(pSequencerSource, pMediaSession);
        }

        private void SetFirstTopology(IMFSequencerSource pSequencerSource, IMFMediaSession pMediaSession)
        {

            IMFMediaSource pMediaSource;
            IMFPresentationDescriptor pPresentationDescriptor;
            IMFMediaSourceTopologyProvider pMediaSourceTopologyProvider;
            IMFTopology pTopology;

            pMediaSource = pSequencerSource as IMFMediaSource;

            // Create the presentation descriptor for the media source.
            pMediaSource.CreatePresentationDescriptor(out pPresentationDescriptor);

            // Get the topology provider from the sequencer source.
            pMediaSourceTopologyProvider = pSequencerSource as IMFMediaSourceTopologyProvider;
            // Get the first topology from the topology provider.
            pMediaSourceTopologyProvider.GetMediaSourceTopology(pPresentationDescriptor, out pTopology);

            // Set the topology on the media session.
            pMediaSession.SetTopology(0, pTopology);
        }
    }
}
