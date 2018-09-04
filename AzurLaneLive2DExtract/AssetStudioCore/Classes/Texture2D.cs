﻿

namespace AssetStudioCore.Classes
{
    class Texture2D : Texture
    {
        public int m_Width;
        public int m_Height;
        public int m_CompleteImageSize;
        public TextureFormat m_TextureFormat;
        public bool m_MipMap;
        public int m_MipCount;
        public bool m_IsReadable;
        public bool m_ReadAllowed;
        public int m_ImageCount;
        public int m_TextureDimension;
        //m_TextureSettings
        public int m_FilterMode;
        public int m_Aniso;
        public float m_MipBias;
        public int m_WrapMode;
        public int m_LightmapFormat;
        public int m_ColorSpace;
        //image dataa
        public int image_data_size;
        public byte[] image_data;
        //m_StreamData
        public uint offset;
        public uint size;
        public string path;

        public Texture2D(AssetPreloadData preloadData) : base(preloadData)
        {
            m_Width = reader.ReadInt32();
            m_Height = reader.ReadInt32();
            m_CompleteImageSize = reader.ReadInt32();
            m_TextureFormat = (TextureFormat)reader.ReadInt32();

            if (version[0] < 5 || (version[0] == 5 && version[1] < 2))
            {
                m_MipMap = reader.ReadBoolean();
            }
            else
            {
                m_MipCount = reader.ReadInt32();
            }

            m_IsReadable = reader.ReadBoolean(); //2.6.0 and up
            m_ReadAllowed = reader.ReadBoolean(); //3.0.0 - 5.4
            //m_StreamingMipmaps 2018.2 and up
            reader.AlignStream(4);
            if (version[0] > 2018 || (version[0] == 2018 && version[1] >= 2)) //2018.2 and up
            {
                var m_StreamingMipmapsPriority = reader.ReadInt32();
            }
            m_ImageCount = reader.ReadInt32();
            m_TextureDimension = reader.ReadInt32();
            //m_TextureSettings
            m_FilterMode = reader.ReadInt32();
            m_Aniso = reader.ReadInt32();
            m_MipBias = reader.ReadSingle();
            if (version[0] >= 2017)//2017.x and up
            {
                int m_WrapU = reader.ReadInt32();
                int m_WrapV = reader.ReadInt32();
                int m_WrapW = reader.ReadInt32();
            }
            else
            {
                m_WrapMode = reader.ReadInt32();
            }
            if (version[0] >= 3)
            {
                m_LightmapFormat = reader.ReadInt32();
                if (version[0] >= 4 || version[1] >= 5)//3.5.0 and up
                {
                    m_ColorSpace = reader.ReadInt32();
                }
            }

            image_data_size = reader.ReadInt32();

            if (image_data_size == 0 && ((version[0] == 5 && version[1] >= 3) || version[0] > 5))//5.3.0 and up
            {
                offset = reader.ReadUInt32();
                size = reader.ReadUInt32();
                image_data_size = (int)size;
                path = reader.ReadAlignedString();
            }

            image_data = reader.ReadBytes(image_data_size);
        }
    }

    public enum TextureFormat
    {
        Alpha8 = 1,
        ARGB4444,
        RGB24,
        RGBA32,
        ARGB32,
        RGB565 = 7,
        R16 = 9,
        DXT1,
        DXT5 = 12,
        RGBA4444,
        BGRA32,
        RHalf,
        RGHalf,
        RGBAHalf,
        RFloat,
        RGFloat,
        RGBAFloat,
        YUY2,
        RGB9e5Float,
        BC4 = 26,
        BC5,
        BC6H = 24,
        BC7,
        DXT1Crunched = 28,
        DXT5Crunched,
        PVRTC_RGB2,
        PVRTC_RGBA2,
        PVRTC_RGB4,
        PVRTC_RGBA4,
        ETC_RGB4,
        ATC_RGB4,
        ATC_RGBA8,
        EAC_R = 41,
        EAC_R_SIGNED,
        EAC_RG,
        EAC_RG_SIGNED,
        ETC2_RGB,
        ETC2_RGBA1,
        ETC2_RGBA8,
        ASTC_RGB_4x4,
        ASTC_RGB_5x5,
        ASTC_RGB_6x6,
        ASTC_RGB_8x8,
        ASTC_RGB_10x10,
        ASTC_RGB_12x12,
        ASTC_RGBA_4x4,
        ASTC_RGBA_5x5,
        ASTC_RGBA_6x6,
        ASTC_RGBA_8x8,
        ASTC_RGBA_10x10,
        ASTC_RGBA_12x12,
        ETC_RGB4_3DS,
        ETC_RGBA8_3DS,
        RG16,
        R8,
        ETC_RGB4Crunched,
        ETC2_RGBA8Crunched,
    }
}