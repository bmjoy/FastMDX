﻿namespace FastMDX {
    public unsafe struct GeosetAnimation : IDataRW {
        public float alpha;
        public uint flags, geosetId;
        public Color color;
        public Transform<float> geosetAlpha;
        public Transform<Vec4> geosetColor;

        void IDataRW.ReadFrom(DataStream ds) {
            var end = ds.Offset + ds.ReadStruct<uint>();

            ds.ReadStruct(ref alpha);
            ds.ReadStruct(ref flags);
            ds.ReadStruct(ref color);
            ds.ReadStruct(ref geosetId);

            while(ds.Offset < end) {
                var tag = ds.ReadStruct<uint>();
                if(tag == (uint)TransformTags.KGAO) {
                    ds.ReadData(ref geosetAlpha);
                } else if(tag == (uint)TransformTags.KGAC) {
                    ds.ReadData(ref geosetColor);
                } else {
                    throw new ParsingException();
                }
            }
        }

        void IDataRW.WriteTo(DataStream ds) {
            var offset = ds.Offset;
            ds.Skip(sizeof(uint));

            ds.WriteStruct(alpha);
            ds.WriteStruct(flags);
            ds.WriteStruct(color);
            ds.WriteStruct(geosetId);

            if(geosetAlpha.HasData) {
                ds.WriteStruct(TransformTags.KGAO);
                ds.WriteData(ref geosetAlpha);
            }

            if(geosetColor.HasData) {
                ds.WriteStruct(TransformTags.KGAC);
                ds.WriteData(ref geosetColor);
            }

            ds.SetValueAt(offset, ds.Offset - offset);
        }
    }
}
