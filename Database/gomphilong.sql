USE [master]
GO
CREATE DATABASE [gomsuphilong]

GO
USE [gomsuphilong]
GO
CREATE TABLE [dbo].[Admins](
	[id] [nvarchar](50) NOT NULL,
	[fullName] [nvarchar](100) NULL,
	[addresss] [nvarchar](100) NULL,
	[phone] [nvarchar](50) NULL,
	[email] [nvarchar](50) NULL,
	[pass] [nvarchar](100) NULL,
	[active] [bit] NULL,
	[subAdmin] [bit] NULL,
	[dateStart] [datetime] NULL,
	[dateEnd] [datetime] NULL,
	[keyFogot] [nvarchar](50) NULL,
 CONSTRAINT [PK__Admins__3213E83F07020F21] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Adv]    Script Date: 2/2/2021 8:15:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adv](
	[id] [nvarchar](50) NOT NULL,
	[advName] [nvarchar](250) NULL,
	[advImage] [nvarchar](250) NULL,
	[advLink] [nvarchar](256) NULL,
	[advType] [int] NULL,
	[advOrder] [int] NULL,
	[advNote] [ntext] NULL,
	[advLang] [nvarchar](50) NULL,
	[advActive] [bit] NULL,
	[createDate] [datetime] NULL,
 CONSTRAINT [PK__Adv__3213E83F0EA330E9] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Brands]    Script Date: 2/2/2021 8:15:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brands](
	[id] [nvarchar](50) NOT NULL,
	[bkName] [nvarchar](450) NULL,
	[bkImage] [nvarchar](250) NULL,
	[bkKey] [nvarchar](450) NULL,
	[numberOder] [int] NULL,
	[note] [ntext] NULL,
 CONSTRAINT [PK__Brands__3213E83F7F60ED59] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categorys]    Script Date: 2/2/2021 8:15:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorys](
	[id] [nvarchar](50) NOT NULL,
	[cateName] [nvarchar](600) NULL,
	[cateKey] [nvarchar](600) NULL,
	[cateLang] [nvarchar](10) NULL,
	[cateLevel] [nvarchar](250) NULL,
	[titleSeo] [nvarchar](800) NULL,
	[keySeo] [nvarchar](800) NULL,
	[desSeo] [nvarchar](800) NULL,
	[cateDescription] [ntext] NULL,
	[cateImage] [nvarchar](450) NULL,
	[cateicon] [nvarchar](250) NULL,
	[catepar_id] [nvarchar](50) NULL,
	[cateOrder] [int] NULL,
	[cate_cap] [int] NULL,
	[cateType] [int] NULL,
	[cateActiveHome] [bit] NULL,
	[cateActive] [bit] NULL,
	[createDate] [datetime] NULL,
	[updateDate] [datetime] NULL,
 CONSTRAINT [PK__Category__3213E83F03317E3D] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 2/2/2021 8:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[id] [nvarchar](50) NOT NULL,
	[proId] [nvarchar](50) NULL,
	[fullName] [nvarchar](250) NULL,
	[email] [nvarchar](50) NULL,
	[phone] [nvarchar](25) NULL,
	[address] [nvarchar](450) NULL,
	[title] [nvarchar](450) NULL,
	[contents] [nvarchar](800) NULL,
	[createDate] [datetime] NULL,
	[active] [bit] NULL,
	[cType] [char](10) NULL,
 CONSTRAINT [PK__Comment__3213E83F0536EE6F] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Config]    Script Date: 2/2/2021 8:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config](
	[id] [nvarchar](50) NOT NULL,
	[hotLine] [nvarchar](250) NULL,
	[phone] [nvarchar](250) NULL,
	[email_Inbox] [nvarchar](50) NULL,
	[email_Send] [nvarchar](50) NULL,
	[emailPass] [nvarchar](50) NULL,
	[mail_Port] [int] NULL,
	[addresss] [nvarchar](350) NULL,
	[shopName] [nvarchar](350) NULL,
	[logoTop] [nvarchar](350) NULL,
	[logoBottom] [nvarchar](350) NULL,
	[linkLogo] [nvarchar](350) NULL,
	[copyRight] [nvarchar](250) NULL,
	[google] [nvarchar](550) NULL,
	[twiter] [nvarchar](550) NULL,
	[youTube] [nvarchar](550) NULL,
	[faceBook] [nvarchar](550) NULL,
	[tiki] [nvarchar](750) NULL,
	[shoppe] [nvarchar](750) NULL,
	[zaloChat] [nvarchar](750) NULL,
	[homeVideo] [ntext] NULL,
	[footerExten] [ntext] NULL,
	[footer] [ntext] NULL,
	[conTact] [ntext] NULL,
	[liveChat] [ntext] NULL,
	[googleAdsense] [ntext] NULL,
	[titleSeo] [nvarchar](800) NULL,
	[keySeo] [nvarchar](800) NULL,
	[desSeo] [nvarchar](800) NULL,
	[viewNewPageList] [int] NULL,
	[viewNewPageDetail] [int] NULL,
	[viewNewPageHome] [int] NULL,
	[viewProPageHome] [int] NULL,
	[viewProPageDetail] [int] NULL,
	[viewProPageList] [int] NULL,
	[priceShip] [int] NULL,
	[conLang] [nvarchar](10) NULL,
	[mapSmall] [ntext] NULL,
	[mapBig] [ntext] NULL,
	[infoPage] [ntext] NULL,
	[favicon] [nvarchar](350) NULL,
	[langDefault] [varchar](10) NULL,
 CONSTRAINT [PK__Config__3213E83F25869641] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 2/2/2021 8:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[id] [nvarchar](50) NOT NULL,
	[fullName] [nvarchar](250) NULL,
	[email] [nvarchar](50) NULL,
	[phone] [nvarchar](25) NULL,
	[address] [nvarchar](450) NULL,
	[title] [nvarchar](450) NULL,
	[contents] [nvarchar](800) NULL,
	[createDate] [datetime] NULL,
	[active] [bit] NULL,
	[referencesId] [nvarchar](50) NULL,
 CONSTRAINT [PK__Contacts__3213E83F29572725] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 2/2/2021 8:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[id] [varchar](50) NOT NULL,
	[fullName] [nvarchar](120) NULL,
	[phone] [varchar](120) NULL,
	[email] [nvarchar](150) NULL,
	[pass] [nvarchar](150) NULL,
	[addresss] [nvarchar](250) NULL,
	[active] [bit] NULL,
	[type] [int] NULL,
	[createDate] [datetime] NULL,
	[wardId] [varchar](50) NULL,
	[districtId] [varchar](50) NULL,
	[provinceId] [varchar](50) NULL,
 CONSTRAINT [PK__Customer__3213E83F32EDCB51] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 2/2/2021 8:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[id] [nvarchar](50) NOT NULL,
	[avata] [nvarchar](450) NULL,
	[name] [nvarchar](450) NULL,
	[descript] [nvarchar](800) NULL,
	[dkey] [nvarchar](450) NULL,
	[contents] [ntext] NULL,
	[titleSeo] [nvarchar](800) NULL,
	[keySeo] [nvarchar](800) NULL,
	[desSeo] [nvarchar](800) NULL,
	[createDate] [datetime] NULL,
	[status] [int] NULL,
	[lang] [nvarchar](50) NULL,
	[dOrder] [int] NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[emails]    Script Date: 2/2/2021 8:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[emails](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](50) NULL,
	[createDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Faq]    Script Date: 2/2/2021 8:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faq](
	[id] [nvarchar](50) NOT NULL,
	[name] [nvarchar](550) NULL,
	[number] [int] NULL,
	[contents] [nvarchar](max) NULL,
	[lang] [nvarchar](50) NULL,
	[createDate] [datetime] NULL,
 CONSTRAINT [PK__Faq__3213E83F03FAAE02] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menus]    Script Date: 2/2/2021 8:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menus](
	[id] [nvarchar](50) NOT NULL,
	[name] [nvarchar](250) NULL,
	[link] [nvarchar](450) NULL,
	[mtypeOf] [nvarchar](50) NULL,
	[titleSeo] [nvarchar](800) NULL,
	[keySeo] [nvarchar](800) NULL,
	[desSeo] [nvarchar](800) NULL,
	[icon] [nvarchar](250) NULL,
	[note] [ntext] NULL,
	[mLang] [nvarchar](10) NULL,
	[mOrder] [int] NULL,
	[par_id] [nvarchar](50) NULL,
	[mtype] [int] NULL,
	[mcap] [int] NULL,
	[mPosition] [int] NULL,
	[active] [bit] NULL,
	[createDate] [datetime] NULL,
	[updateDate] [datetime] NULL,
 CONSTRAINT [PK__Menus__3213E83F164452B1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[News]    Script Date: 2/2/2021 8:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[id] [nvarchar](50) NOT NULL,
	[groupId] [nvarchar](50) NULL,
	[newImage] [nvarchar](256) NULL,
	[title] [nvarchar](800) NULL,
	[descript] [nvarchar](800) NULL,
	[contents] [ntext] NULL,
	[new_key] [nvarchar](800) NULL,
	[titleSeo] [nvarchar](800) NULL,
	[keySeo] [nvarchar](800) NULL,
	[desSeo] [nvarchar](800) NULL,
	[newLang] [nvarchar](10) NULL,
	[newOrder] [int] NULL,
	[status] [int] NULL,
	[viewCount] [int] NULL,
	[newHot] [bit] NULL,
	[newNew] [bit] NULL,
	[newFile] [nvarchar](256) NULL,
	[createDate] [datetime] NULL,
	[updateDate] [datetime] NULL,
 CONSTRAINT [PK__News__3213E83F1A14E395] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Oderdt]    Script Date: 2/2/2021 8:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Oderdt](
	[id] [varchar](50) NOT NULL,
	[oderId] [varchar](50) NULL,
	[proId] [varchar](50) NULL,
	[quantity] [int] NULL,
	[priceNow] [int] NULL,
	[priceCount] [int] NULL,
 CONSTRAINT [PK__Oderdt__3213E83F38F96B7A] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Oders]    Script Date: 2/2/2021 8:15:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Oders](
	[id] [varchar](50) NOT NULL,
	[cusId] [nvarchar](50) NULL,
	[total] [int] NULL,
	[createDate] [datetime] NULL,
	[status] [int] NULL,
	[priceShip] [int] NULL,
	[noteSiteAdmin] [nvarchar](500) NULL,
	[noteSite] [nvarchar](500) NULL,
	[updateDate] [datetime] NULL,
	[updateBy] [nvarchar](50) NULL,
 CONSTRAINT [PK__Oders__3213E83F4FFA80D8] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Partner]    Script Date: 2/2/2021 8:15:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partner](
	[id] [nvarchar](50) NOT NULL,
	[pName] [nvarchar](250) NULL,
	[pImage] [nvarchar](350) NULL,
	[pNote] [ntext] NULL,
	[numberOder] [int] NULL,
	[pLink] [nvarchar](450) NULL,
	[groupId] [nvarchar](50) NULL,
	[lang] [nchar](10) NULL,
 CONSTRAINT [PK__Partner__3213E83FDDCF8074] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2/2/2021 8:15:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[id] [nvarchar](50) NOT NULL,
	[pro_name] [nvarchar](800) NULL,
	[pro_key] [nvarchar](800) NULL,
	[proPrice] [int] NULL,
	[proPrice_sale] [int] NULL,
	[pro_view] [int] NULL,
	[proOrder] [int] NULL,
	[proAvata] [nvarchar](450) NULL,
	[proImages5] [nvarchar](450) NULL,
	[proImages4] [nvarchar](450) NULL,
	[proImages3] [nvarchar](450) NULL,
	[proImages2] [nvarchar](450) NULL,
	[proImages1] [nvarchar](450) NULL,
	[proFile] [nvarchar](456) NULL,
	[brank] [nvarchar](250) NULL,
	[desPro] [nvarchar](800) NULL,
	[proContentTab1] [ntext] NULL,
	[proContentTab2] [ntext] NULL,
	[introContent] [ntext] NULL,
	[pro_home] [bit] NULL,
	[pro_hot] [bit] NULL,
	[active] [bit] NULL,
	[titleSeo] [nvarchar](800) NULL,
	[keySeo] [nvarchar](800) NULL,
	[desSeo] [nvarchar](800) NULL,
	[pLang] [nvarchar](10) NULL,
	[cateId] [nvarchar](50) NULL,
	[createDate] [datetime] NULL,
	[updateDate] [datetime] NULL,
	[brandId] [varchar](50) NULL,
 CONSTRAINT [PK__Products__3213E83F2D27B809] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProTag]    Script Date: 2/2/2021 8:15:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProTag](
	[idPT] [varchar](50) NOT NULL,
	[proId] [varchar](50) NULL,
	[tagId] [varchar](50) NULL,
 CONSTRAINT [PK_ProTag] PRIMARY KEY CLUSTERED 
(
	[idPT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SayWe]    Script Date: 2/2/2021 8:15:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SayWe](
	[id] [nvarchar](50) NOT NULL,
	[fullName] [nvarchar](150) NULL,
	[email] [nvarchar](50) NULL,
	[phone] [nvarchar](50) NULL,
	[addresss] [nvarchar](150) NULL,
	[job] [nvarchar](150) NULL,
	[avata] [nvarchar](250) NULL,
	[contents] [ntext] NULL,
	[active] [bit] NULL,
	[slang] [nvarchar](10) NULL,
	[createDate] [datetime] NULL,
	[numberOder] [int] NULL,
 CONSTRAINT [PK__SayWe__3213E83F406752DC] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Slide]    Script Date: 2/2/2021 8:15:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Slide](
	[id] [nvarchar](50) NOT NULL,
	[name] [nvarchar](150) NULL,
	[images] [nvarchar](250) NULL,
	[contents] [ntext] NULL,
	[active] [bit] NULL,
	[slang] [nvarchar](50) NULL,
	[sLink] [nvarchar](250) NULL,
	[numberOder] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Support]    Script Date: 2/2/2021 8:15:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Support](
	[id] [nvarchar](50) NOT NULL,
	[sType] [int] NULL,
	[nick] [nvarchar](200) NULL,
	[phone] [nvarchar](25) NULL,
	[email] [nvarchar](100) NULL,
	[addresss] [nvarchar](100) NULL,
	[fullName] [nvarchar](100) NULL,
	[numberOder] [int] NULL,
 CONSTRAINT [PK__Support__3213E83F2DC2A16C] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tagpro]    Script Date: 2/2/2021 8:15:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tagpro](
	[tagId] [nvarchar](50) NOT NULL,
	[tagName] [nvarchar](250) NULL,
	[tagKey] [nvarchar](250) NULL,
	[typeTag] [int] NULL,
	[tagOrder] [int] NULL,
	[lang] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tagpro] PRIMARY KEY CLUSTERED 
(
	[tagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WhyChooseUss]    Script Date: 2/2/2021 8:15:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WhyChooseUss](
	[id] [nvarchar](50) NOT NULL,
	[title] [nvarchar](250) NULL,
	[image] [nvarchar](350) NULL,
	[contents] [ntext] NULL,
	[active] [bit] NULL,
	[slang] [nvarchar](10) NULL,
	[createDate] [datetime] NULL,
	[numberOder] [int] NULL,
 CONSTRAINT [PK__WhyChoos__3213E83FA01E61E2] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Admins] ([id], [fullName], [addresss], [phone], [email], [pass], [active], [subAdmin], [dateStart], [dateEnd], [keyFogot]) VALUES (N'1', N'Hiệp', N'HH', N'HH', N'admin@gmail.com', N'14e1b600b1fd579f47433b88e8d85291', 1, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[Admins] ([id], [fullName], [addresss], [phone], [email], [pass], [active], [subAdmin], [dateStart], [dateEnd], [keyFogot]) VALUES (N'2', N'hiep1', N'hn', NULL, N'hiep.clhd@gmail.com', N'14e1b600b1fd579f47433b88e8d85291', 1, 1, NULL, NULL, NULL)
GO
INSERT [dbo].[Admins] ([id], [fullName], [addresss], [phone], [email], [pass], [active], [subAdmin], [dateStart], [dateEnd], [keyFogot]) VALUES (N'f8c2f276-66d2-4e50-80f6-0041885f2af4', N'Hội', N'HD', N'0868.977.398', N'nvhoihn.it@gmai.com', N'14e1b600b1fd579f47433b88e8d85291', 1, 0, CAST(N'2019-07-28T16:31:48.373' AS DateTime), CAST(N'2019-07-28T18:31:48.373' AS DateTime), N'ab7387bb-fb9e-40ec-93a6-41ace159d11b')
GO
INSERT [dbo].[Adv] ([id], [advName], [advImage], [advLink], [advType], [advOrder], [advNote], [advLang], [advActive], [createDate]) VALUES (N'1859a619-97de-4302-8061-61e563d4ce25', N'Gốm sứ', N'/uploads/advi/bt-1.jpg', NULL, 2, 1, NULL, N'vi', 1, CAST(N'2021-01-31T22:04:16.523' AS DateTime))
GO
INSERT [dbo].[Adv] ([id], [advName], [advImage], [advLink], [advType], [advOrder], [advNote], [advLang], [advActive], [createDate]) VALUES (N'3e21ea34-89d2-4c75-a860-ff404da5754d', N'Gốm bát tràng', N'/uploads/advi/bt-4.jpg', NULL, 2, 5, NULL, N'vi', 1, CAST(N'2021-01-31T22:08:23.563' AS DateTime))
GO
INSERT [dbo].[Adv] ([id], [advName], [advImage], [advLink], [advType], [advOrder], [advNote], [advLang], [advActive], [createDate]) VALUES (N'7032e766-0254-4a2d-9fa0-dc8a33c1d75b', N'Gốm bát tràng', N'/uploads/advi/bt-4.jpg', NULL, 2, 2, NULL, N'vi', 1, CAST(N'2021-01-31T22:04:48.127' AS DateTime))
GO
INSERT [dbo].[Adv] ([id], [advName], [advImage], [advLink], [advType], [advOrder], [advNote], [advLang], [advActive], [createDate]) VALUES (N'8b479eb8-9f25-4119-8bcc-48a0af4b1d56', N'Gốm Chu Đậu', N'/uploads/advi/bt-1.jpg', NULL, 2, 3, NULL, N'vi', 1, CAST(N'2021-01-31T22:08:15.063' AS DateTime))
GO
INSERT [dbo].[Brands] ([id], [bkName], [bkImage], [bkKey], [numberOder], [note]) VALUES (N'8858ea2b-a01f-43ac-859d-7e838aca5c00', N'Gốm Chu Đậu', N'/', N'gom-chu-dau', 2, NULL)
GO
INSERT [dbo].[Brands] ([id], [bkName], [bkImage], [bkKey], [numberOder], [note]) VALUES (N'c17c832c-8397-4bf1-b812-54bbd06668df', N'Gốm Bát Tràng', N'/', N'gom-bat-trang', 1, NULL)
GO
INSERT [dbo].[Categorys] ([id], [cateName], [cateKey], [cateLang], [cateLevel], [titleSeo], [keySeo], [desSeo], [cateDescription], [cateImage], [cateicon], [catepar_id], [cateOrder], [cate_cap], [cateType], [cateActiveHome], [cateActive], [createDate], [updateDate]) VALUES (N'769dacd5-6cf3-4e9f-8c4f-f2a8b64c3b8c', N'SẢN PHẨM TRANG TRÍ', N'san-pham-trang-tri', N'vi', N'', N'', N'', N'', N'', N'/uploads/category/thiet-bi-tu-dong.jpg', N'', N'-1', 1, 1, 1, 1, 1, CAST(N'2021-01-31T18:23:40.893' AS DateTime), CAST(N'2021-01-31T18:25:34.380' AS DateTime))
GO
INSERT [dbo].[Categorys] ([id], [cateName], [cateKey], [cateLang], [cateLevel], [titleSeo], [keySeo], [desSeo], [cateDescription], [cateImage], [cateicon], [catepar_id], [cateOrder], [cate_cap], [cateType], [cateActiveHome], [cateActive], [createDate], [updateDate]) VALUES (N'95c980b5-2ad6-45ee-8d14-0d7f6827100e', N'QUÀ TẶNG HỘI NGHỊ', N'qua-tang-hoi-nghi', N'vi', N'', N'', N'', N'', N'', N'/uploads/category/thiet-bi-tu-dong.jpg', N'', N'-1', 3, 1, 1, 1, 1, CAST(N'2021-01-31T18:24:01.113' AS DateTime), CAST(N'2021-01-31T18:25:28.363' AS DateTime))
GO
INSERT [dbo].[Categorys] ([id], [cateName], [cateKey], [cateLang], [cateLevel], [titleSeo], [keySeo], [desSeo], [cateDescription], [cateImage], [cateicon], [catepar_id], [cateOrder], [cate_cap], [cateType], [cateActiveHome], [cateActive], [createDate], [updateDate]) VALUES (N'a181dad0-69c1-472c-bb80-01ed0483791e', N'Kiến thức đồ thờ cúng', N'kien-thuc-do-tho-cung', N'vi', N'', N'', N'', N'', N'', NULL, N'', N'-1', 2, 1, 2, 0, 1, CAST(N'2021-01-31T21:02:13.493' AS DateTime), CAST(N'2021-01-31T21:02:13.493' AS DateTime))
GO
INSERT [dbo].[Categorys] ([id], [cateName], [cateKey], [cateLang], [cateLevel], [titleSeo], [keySeo], [desSeo], [cateDescription], [cateImage], [cateicon], [catepar_id], [cateOrder], [cate_cap], [cateType], [cateActiveHome], [cateActive], [createDate], [updateDate]) VALUES (N'bf3707be-4501-4d4b-a03b-4672d5a0ff8f', N'Kiến thức gốm sứ', N'kien-thuc-gom-su', N'vi', N'', N'', N'', N'', N'', NULL, N'', N'-1', 1, 1, 2, 0, 1, CAST(N'2021-01-31T21:01:49.620' AS DateTime), CAST(N'2021-01-31T21:01:49.620' AS DateTime))
GO
INSERT [dbo].[Categorys] ([id], [cateName], [cateKey], [cateLang], [cateLevel], [titleSeo], [keySeo], [desSeo], [cateDescription], [cateImage], [cateicon], [catepar_id], [cateOrder], [cate_cap], [cateType], [cateActiveHome], [cateActive], [createDate], [updateDate]) VALUES (N'c543570e-896f-4e7d-9373-60a4fb7cce17', N'GỐM SỨ PHONG THỦY', N'gom-su-phong-thuy', N'vi', N'', N'', N'', N'', N'', N'/uploads/category/thiet-bi-tu-dong.jpg', N'', N'-1', 2, 1, 1, 1, 1, CAST(N'2021-01-31T18:23:51.393' AS DateTime), CAST(N'2021-01-31T18:25:08.617' AS DateTime))
GO
INSERT [dbo].[Config] ([id], [hotLine], [phone], [email_Inbox], [email_Send], [emailPass], [mail_Port], [addresss], [shopName], [logoTop], [logoBottom], [linkLogo], [copyRight], [google], [twiter], [youTube], [faceBook], [tiki], [shoppe], [zaloChat], [homeVideo], [footerExten], [footer], [conTact], [liveChat], [googleAdsense], [titleSeo], [keySeo], [desSeo], [viewNewPageList], [viewNewPageDetail], [viewNewPageHome], [viewProPageHome], [viewProPageDetail], [viewProPageList], [priceShip], [conLang], [mapSmall], [mapBig], [infoPage], [favicon], [langDefault]) VALUES (N'1', N'0989922358', N'0989922358', N'dinhthanhsakura@gmail.com', N'dinhthanhsakura@gmail.com', N'khongcopass', 465, N'Cùng xem những hình ảnh mới nhất ctại hệ thống cửa hàng Gốm Sứ PhiLong', N'Gốm sứ PhiLong', N'/uploads/advi/logo.jpg', N'/uploads/advi/banner_mobile.jpg', N'/', N'© Bản quyền thuộc về Gốm sứ phiLong ', N'https://www.google.com.vn', N'https://www.google.com.vn', N'https://www.youtube.com/', N'https://www.facebook.com/', N'', N'', N' 0947 203 581', N'', N'<h4><span>Xưởng sản xuất</span></h4>

<ul class="list-menu infomation" style="display: block;">
	<li class="item">
	<div class="address">Showroom 1: Khu 15 thị trấn Lai Cách - Cẩm Giàng - Hải Dương</div>

	<div class="address">&nbsp;</div>
	</li>
</ul>
', N'<h4><span>GỐM SỨ PHILONG</span></h4>

<ul class="list-menu infomation" style="display: block;">
	<li class="item">
	<div class="company">Công ty cổ phần gốm sứ phiLong</div>

	<div class="address">Địa chỉ: Khu 15 thị trấn Lai Cách - Cẩm Giàng - Hải Dương</div>

	<div class="hotline">Hotline: <a href="tel:0356998866" title="Phone">(+84) 0989922358</a></div>
	</li>
</ul>
', N'<h4><span>GỐM SỨ PHILONG</span></h4>

<ul class="list-menu infomation" style="display: block;">
	<li class="item">
	<div class="company">Công ty cổ phần gốm sứ phiLong</div>

	<div class="address">Địa chỉ: Khu 15 thị trấn Lai Cách - Cẩm Giàng - Hải Dương</div>

	<div class="hotline">Hotline: <a href="tel:0356998866" title="Phone">(+84) 0989922358</a></div>
	</li>
</ul>
', N'', N'', N'Gốm sứ PhiLong', N'Gốm sứ PhiLong', N'Gốm sứ PhiLong', 6, 3, 3, 4, 4, 8, 1, N'vi', N'<p><iframe allowfullscreen="" aria-hidden="false" frameborder="0" height="450" src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3725.868417507206!2d105.84429401488224!3d20.957798686036927!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3135adb389967255%3A0xe40edf6d30c6f0bf!2zMTAwIMSQLiBUcuG6p24gVGjhu6cgxJDhu5ksIEhvw6BuZyBMaeG7h3QsIEhhaSBCw6AgVHLGsG5nLCBIw6AgTuG7mWksIFZp4buHdCBOYW0!5e0!3m2!1svi!2s!4v1607569058367!5m2!1svi!2s" style="border:0;" tabindex="0" width="600"></iframe></p>
', N'<p><iframe allowfullscreen="" aria-hidden="false" frameborder="0" height="310" src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3725.868417507206!2d105.84429401488224!3d20.957798686036927!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3135adb389967255%3A0xe40edf6d30c6f0bf!2zMTAwIMSQLiBUcuG6p24gVGjhu6cgxJDhu5ksIEhvw6BuZyBMaeG7h3QsIEhhaSBCw6AgVHLGsG5nLCBIw6AgTuG7mWksIFZp4buHdCBOYW0!5e0!3m2!1svi!2s!4v1607569058367!5m2!1svi!2s" style="border:0;" tabindex="0" width="550"></iframe></p>
', N'<div class="footer-widget">
<h4><span>Kết nối với chúng tôi</span></h4>

<p>Với sứ mệnh giữ gìn và tôn vinh vẻ đẹp Việt, trong suốt 14 năm qua.Chúng tôi mong muốn tạo nên Tập đoàn Thẩm mỹ uy tín nhất tại Việt Nam.</p>
</div>
', N'', NULL)
GO
INSERT [dbo].[Customers] ([id], [fullName], [phone], [email], [pass], [addresss], [active], [type], [createDate], [wardId], [districtId], [provinceId]) VALUES (N'1ac74dcc-ea73-48a6-aa13-a24e054bc4ba', N'Vu Dinh Hong', N'0388136579', N'vdhdaihocluat88@gmail.com', NULL, NULL, 1, 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Customers] ([id], [fullName], [phone], [email], [pass], [addresss], [active], [type], [createDate], [wardId], [districtId], [provinceId]) VALUES (N'49b91069-8438-4696-86e7-a18787a7e26b', N'Hiep test', N'0964488399', N'u@gmail.com', NULL, NULL, 1, 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Menus] ([id], [name], [link], [mtypeOf], [titleSeo], [keySeo], [desSeo], [icon], [note], [mLang], [mOrder], [par_id], [mtype], [mcap], [mPosition], [active], [createDate], [updateDate]) VALUES (N'097a343f-403c-413e-a0d0-f08c6f26a71e', N'Trang chủ', N'/', N'-1', N'', N'', N'', N'', N'', N'vi', 1, N'-1', 1, 1, 2, 1, CAST(N'2019-09-23T13:33:45.743' AS DateTime), CAST(N'2019-09-23T13:33:45.743' AS DateTime))
GO
INSERT [dbo].[Menus] ([id], [name], [link], [mtypeOf], [titleSeo], [keySeo], [desSeo], [icon], [note], [mLang], [mOrder], [par_id], [mtype], [mcap], [mPosition], [active], [createDate], [updateDate]) VALUES (N'17699191-0066-4d81-9610-0218f568cc55', N'Liên hệ', N'http://tpa-edu.com.vn/lien-he', N'-1', N'', N'', N'', N'', N'', N'vi', 5, N'-1', 1, 1, 2, 1, CAST(N'2019-09-23T14:26:58.407' AS DateTime), CAST(N'2019-09-23T14:26:58.407' AS DateTime))
GO
INSERT [dbo].[Menus] ([id], [name], [link], [mtypeOf], [titleSeo], [keySeo], [desSeo], [icon], [note], [mLang], [mOrder], [par_id], [mtype], [mcap], [mPosition], [active], [createDate], [updateDate]) VALUES (N'2b68c0a0-5742-455d-8867-694e25587766', N'Gốm sứ phong thủy', N'/danh-muc/gom-su-phong-thuy', N'1', N'', N'', N'', N'', N'', N'vi', 2, N'99f6e697-8dff-444c-9e36-b6a3f9e53721', 3, 2, 1, 1, CAST(N'2021-01-31T20:46:20.687' AS DateTime), CAST(N'2021-01-31T20:46:20.687' AS DateTime))
GO
INSERT [dbo].[Menus] ([id], [name], [link], [mtypeOf], [titleSeo], [keySeo], [desSeo], [icon], [note], [mLang], [mOrder], [par_id], [mtype], [mcap], [mPosition], [active], [createDate], [updateDate]) VALUES (N'416cf4bb-1b0d-4aba-bf42-5dcef043c1fc', N'TIN TỨC', N'/tin-tuc', N'2', N'', N'', N'', N'', N'', N'vi', 6, N'-1', 3, 1, 1, 1, CAST(N'2019-09-15T23:21:42.827' AS DateTime), CAST(N'2020-04-28T23:36:08.637' AS DateTime))
GO
INSERT [dbo].[Menus] ([id], [name], [link], [mtypeOf], [titleSeo], [keySeo], [desSeo], [icon], [note], [mLang], [mOrder], [par_id], [mtype], [mcap], [mPosition], [active], [createDate], [updateDate]) VALUES (N'5a59b873-8170-45a6-bb8a-1a06d0ac28cf', N'DỊCH VỤ', N'/sites/dich-vu', N'-1', N'Gốm sứ PhiLong', N'Gốm sứ PhiLong', N'Gốm sứ PhiLong', N'', N'<p><font size="2"><b>Nội dung đang cập nhật...</b></font></p>
', N'vi', 9, N'-1', 2, 1, 1, 1, CAST(N'2019-09-19T23:07:07.363' AS DateTime), CAST(N'2021-01-31T22:01:28.270' AS DateTime))
GO
INSERT [dbo].[Menus] ([id], [name], [link], [mtypeOf], [titleSeo], [keySeo], [desSeo], [icon], [note], [mLang], [mOrder], [par_id], [mtype], [mcap], [mPosition], [active], [createDate], [updateDate]) VALUES (N'5f367dfa-666f-496a-88ef-72d77c8c4126', N'Thương hiệu', N'/danh-muc', N'1', N'', N'', N'', N'', N'', N'vi', 4, N'-1', 3, 1, 1, 1, CAST(N'2021-01-31T20:47:37.133' AS DateTime), CAST(N'2021-01-31T20:47:37.133' AS DateTime))
GO
INSERT [dbo].[Menus] ([id], [name], [link], [mtypeOf], [titleSeo], [keySeo], [desSeo], [icon], [note], [mLang], [mOrder], [par_id], [mtype], [mcap], [mPosition], [active], [createDate], [updateDate]) VALUES (N'6ae97721-78ec-4072-bdaa-1fc1d0635a08', N'Gốm Chu Đậu', N'/thuong-hieu/gom-chu-dau', N'3', N'', N'', N'', N'', N'', N'vi', 2, N'5f367dfa-666f-496a-88ef-72d77c8c4126', 3, 2, 1, 1, CAST(N'2021-01-31T20:58:56.543' AS DateTime), CAST(N'2021-01-31T20:58:56.543' AS DateTime))
GO
INSERT [dbo].[Menus] ([id], [name], [link], [mtypeOf], [titleSeo], [keySeo], [desSeo], [icon], [note], [mLang], [mOrder], [par_id], [mtype], [mcap], [mPosition], [active], [createDate], [updateDate]) VALUES (N'6aea32ad-b778-4f5f-b9a8-f19999c90838', N'Gốm Bát Tràng', N'/thuong-hieu/gom-bat-trang', N'3', N'', N'', N'', N'', N'', N'vi', 1, N'5f367dfa-666f-496a-88ef-72d77c8c4126', 3, 2, 1, 1, CAST(N'2021-01-31T20:58:29.153' AS DateTime), CAST(N'2021-01-31T20:59:10.217' AS DateTime))
GO
INSERT [dbo].[Menus] ([id], [name], [link], [mtypeOf], [titleSeo], [keySeo], [desSeo], [icon], [note], [mLang], [mOrder], [par_id], [mtype], [mcap], [mPosition], [active], [createDate], [updateDate]) VALUES (N'6d529e7f-8159-418f-a913-f49cd2b472fc', N'Quà tặng hội nghị', N'/danh-muc/qua-tang-hoi-nghi', N'1', N'', N'', N'', N'', N'', N'vi', 3, N'99f6e697-8dff-444c-9e36-b6a3f9e53721', 3, 2, 1, 1, CAST(N'2021-01-31T20:46:42.467' AS DateTime), CAST(N'2021-01-31T20:46:42.467' AS DateTime))
GO
INSERT [dbo].[Menus] ([id], [name], [link], [mtypeOf], [titleSeo], [keySeo], [desSeo], [icon], [note], [mLang], [mOrder], [par_id], [mtype], [mcap], [mPosition], [active], [createDate], [updateDate]) VALUES (N'78741423-77d1-488a-8e39-09eb375488e7', N'Giới thiệu', N'http://tpa-edu.com.vn/sites/gioi-thieu', N'-1', N'', N'', N'', N'', N'', N'vi', 4, N'-1', 1, 1, 2, 1, CAST(N'2019-09-23T14:26:25.137' AS DateTime), CAST(N'2019-09-23T14:26:25.137' AS DateTime))
GO
INSERT [dbo].[Menus] ([id], [name], [link], [mtypeOf], [titleSeo], [keySeo], [desSeo], [icon], [note], [mLang], [mOrder], [par_id], [mtype], [mcap], [mPosition], [active], [createDate], [updateDate]) VALUES (N'7b2ef8c1-3ee9-4d4e-8fa7-3dc08b17e2d4', N'LIÊN HỆ', N'/lien-he', N'7', N'', N'', N'', N'', N'', N'vi', 19, N'-1', 3, 1, 1, 1, CAST(N'2019-09-15T23:21:49.387' AS DateTime), CAST(N'2020-04-28T23:36:35.510' AS DateTime))
GO
INSERT [dbo].[Menus] ([id], [name], [link], [mtypeOf], [titleSeo], [keySeo], [desSeo], [icon], [note], [mLang], [mOrder], [par_id], [mtype], [mcap], [mPosition], [active], [createDate], [updateDate]) VALUES (N'829ff585-303f-41fe-a2b9-ef9847a9f3d9', N'Kiến thức gốm sứ', N'/tin-tuc/kien-thuc-gom-su', N'2', N'', N'', N'', N'', N'', N'vi', 1, N'416cf4bb-1b0d-4aba-bf42-5dcef043c1fc', 3, 2, 1, 1, CAST(N'2019-09-19T10:34:37.140' AS DateTime), CAST(N'2021-01-31T21:02:33.840' AS DateTime))
GO
INSERT [dbo].[Menus] ([id], [name], [link], [mtypeOf], [titleSeo], [keySeo], [desSeo], [icon], [note], [mLang], [mOrder], [par_id], [mtype], [mcap], [mPosition], [active], [createDate], [updateDate]) VALUES (N'85dc16b4-4733-4314-aaeb-7d83948c3ff4', N'Chính sách mua hàng', N'/sites/chinh-sach-mua-hang', N'-1', N'', N'', N'', N'', N'<p>Đang cập nhật nội dung...</p>
', N'vi', 10, N'-1', 2, 1, 1, 1, CAST(N'2021-01-31T21:00:33.673' AS DateTime), CAST(N'2021-01-31T21:00:33.673' AS DateTime))
GO
INSERT [dbo].[Menus] ([id], [name], [link], [mtypeOf], [titleSeo], [keySeo], [desSeo], [icon], [note], [mLang], [mOrder], [par_id], [mtype], [mcap], [mPosition], [active], [createDate], [updateDate]) VALUES (N'8d55c730-f580-486c-86de-718b2a9401b6', N'Kiến thức đồ thờ cúng', N'/tin-tuc/kien-thuc-do-tho-cung', N'2', N'', N'', N'', N'', N'', N'vi', 2, N'416cf4bb-1b0d-4aba-bf42-5dcef043c1fc', 3, 2, 1, 1, CAST(N'2020-05-03T18:47:12.833' AS DateTime), CAST(N'2021-01-31T21:02:51.120' AS DateTime))
GO
INSERT [dbo].[Menus] ([id], [name], [link], [mtypeOf], [titleSeo], [keySeo], [desSeo], [icon], [note], [mLang], [mOrder], [par_id], [mtype], [mcap], [mPosition], [active], [createDate], [updateDate]) VALUES (N'99f6e697-8dff-444c-9e36-b6a3f9e53721', N'SẢN PHẨM', N'/danh-muc', N'1', N'', N'', N'', N'', N'', N'vi', 3, N'-1', 3, 1, 1, 1, CAST(N'2019-09-15T23:21:14.160' AS DateTime), CAST(N'2020-04-28T23:35:03.087' AS DateTime))
GO
INSERT [dbo].[Menus] ([id], [name], [link], [mtypeOf], [titleSeo], [keySeo], [desSeo], [icon], [note], [mLang], [mOrder], [par_id], [mtype], [mcap], [mPosition], [active], [createDate], [updateDate]) VALUES (N'a4f90a7d-a5e0-4eb9-b3dc-e4e8330871c5', N'Tin tức', N'http://tpa-edu.com.vn/tin-tuc', N'-1', N'', N'', N'', N'', N'', N'vi', 3, N'-1', 1, 1, 2, 1, CAST(N'2019-09-23T14:25:21.193' AS DateTime), CAST(N'2019-09-23T14:25:21.193' AS DateTime))
GO
INSERT [dbo].[Menus] ([id], [name], [link], [mtypeOf], [titleSeo], [keySeo], [desSeo], [icon], [note], [mLang], [mOrder], [par_id], [mtype], [mcap], [mPosition], [active], [createDate], [updateDate]) VALUES (N'abbb8a12-0aba-4ed7-a01e-1c3c5b708dac', N'Sản phẩm trang trí', N'/danh-muc/san-pham-trang-tri', N'1', N'', N'', N'', N'', N'', N'vi', 1, N'99f6e697-8dff-444c-9e36-b6a3f9e53721', 3, 2, 1, 1, CAST(N'2021-01-31T20:45:57.897' AS DateTime), CAST(N'2021-01-31T20:45:57.897' AS DateTime))
GO
INSERT [dbo].[Menus] ([id], [name], [link], [mtypeOf], [titleSeo], [keySeo], [desSeo], [icon], [note], [mLang], [mOrder], [par_id], [mtype], [mcap], [mPosition], [active], [createDate], [updateDate]) VALUES (N'b25553e5-e353-4fd4-b70a-ca15af95d0a6', N'TRANG CHỦ', N'/', N'-1', N'', N'', N'', N'', N'', N'vi', 0, N'-1', 1, 1, 1, 1, CAST(N'2020-03-22T19:48:11.613' AS DateTime), CAST(N'2020-04-28T23:35:35.557' AS DateTime))
GO
INSERT [dbo].[Menus] ([id], [name], [link], [mtypeOf], [titleSeo], [keySeo], [desSeo], [icon], [note], [mLang], [mOrder], [par_id], [mtype], [mcap], [mPosition], [active], [createDate], [updateDate]) VALUES (N'f677618e-f2c3-487d-99a6-970b7387392d', N'GIỚI THIỆU', N'/sites/gioi-thieu', N'-1', N'Cầu thang nội thất', N'Cầu thang, ban công, tủ bếp, cổng sắt', N'Cầu thang, ban công, tủ bếp, cổng sắt', N'', N'<p>Đang cập nhật</p>
', N'vi', 1, N'-1', 2, 1, 1, 1, CAST(N'2019-12-12T13:29:59.553' AS DateTime), CAST(N'2020-05-30T09:47:37.757' AS DateTime))
GO
INSERT [dbo].[News] ([id], [groupId], [newImage], [title], [descript], [contents], [new_key], [titleSeo], [keySeo], [desSeo], [newLang], [newOrder], [status], [viewCount], [newHot], [newNew], [newFile], [createDate], [updateDate]) VALUES (N'66d99926-30e3-40bc-a352-47fb4b0f1428', N'a181dad0-69c1-472c-bb80-01ed0483791e', N'/uploads/news/c12024-300x300.jpg', N'LÀM SAO ĐỂ BÀI TRÍ, SẮP XẾP ĐƯỢC BAN THỜ ĐÚNG Ý, VỪA ĐẦY ĐỦ VỪA HÀI HÒA VÀ ĐỒNG BỘ', N'Làm sao để bài trí, sắp xếp được ban thờ đúng ý, vừa đầy đủ vừa hài hòa và đồng bộ Trên một ban thờ cần lựa chọn những đồ thờ nào, kích thước ra sao, cách bài trí như thế nào? Gia chủ hãy tham khảo những thông tin tư vấn dưới đây để hiểu hơn về cách lựa chọn và sắp xếp đồ thờ phù hợp. Hiện nay, bộ đồ thờ gia tiên Bát Tràng gồm những loại sau 1. Bộ đồ thờ men xanh lam cổ 2. Bộ đồ thờ men rạn cổ 3. Bộ đồ thờ đắp nổi men...', N'<h2>Trên một ban thờ cần lựa chọn những đồ thờ nào, kích thước ra sao, cách bài trí như thế nào? Gia chủ hãy tham khảo những thông tin tư vấn dưới đây để hiểu hơn về cách lựa chọn và sắp xếp đồ thờ phù hợp.</h2>

<p>Hiện nay, bộ đồ thờ gia tiên Bát Tràng gồm những loại sau</p>

<p>1. Bộ đồ thờ men xanh lam cổ</p>

<p>2. Bộ đồ thờ men rạn cổ</p>

<p>3. Bộ đồ thờ đắp nổi men rạn cao cấp</p>

<p>4. Bộ đồ thờ vẽ vàng kim cao cấp</p>

<p>5. Bộ đồ thờ xanh lục bảo bọc đồng cao cấp</p>

<p>6. Bộ đồ thờ men hoàng thổ cao cấp</p>

<p><a id="Những đồ thờ cần có trên ban thờ theo truyền thống, dựa vào kích thước ban thờ để lựa chọn bát hương cho phù hợp" name="Những đồ thờ cần có trên ban thờ theo truyền thống, dựa vào kích thước ban thờ để lựa chọn bát hương cho phù hợp">Những đồ thờ cần có trên ban thờ theo truyền thống, dựa vào kích thước ban thờ để lựa chọn bát hương cho phù hợp</a></p>

<p><font color="#e74c3c">Đối với ban thờ có kích thước dài 1m57, cao 1m27, quý khách nên chọn bát hương ở giữa phi 18cm, 2 bát hương còn lại phi 16cm</font></p>

<p><font color="#e74c3c">Đối với ban thờ có kích thước dài 1m97, 2m17, cao 1m27, quý khách nên chọn bát hương ở giữa phi 20cm, 2 bát hương còn lại phi 18cm</font></p>

<p>Theo truyền thống, một ban thờ cơ bản sẽ có những đồ thờ cơ bản đó là:</p>

<p>+ Bát hương: Nếu gia đình chỉ thờ thần linh, gia chủ đặt 1 bát hương trên bàn thờ. Gia đình thờ gia tiên sẽ đặt 3 bát hương, 1 bát hương cỡ lớn và 2 bát hương nhỏ</p>

<p>Khi đặt bát hương trên ban thờ, gia chủ có thể đặt trên đế bát hương bằng sứ, đế bát hương bằng gỗ hoặc đặt trên tam cấp cho đẹp.</p>

<p>+ Lộc bình (lọ hoa): Ngoài bát hương, trên ban thờ đặt thêm lọ hoa nhỏ để cắm hoa tươi. Gia chủ có thể đặt lọ hoa theo số lượng tùy ý ở 2 bên ban thờ.</p>

<p>+ Mâm bồng (Đĩa đựng lễ vật): Một ban thờ cơ bản sẽ có 1 mâm bồng đặt chính giữa ban thờ. Mâm bồng có thể đặt hoa quả, bánh kẹo, trầu cau, tiền vàng,&hellip;</p>

<p>+ Nậm rượu: Nậm đựng rượu cúng bằng gốm sứ. Bàn thờ nhỏ gia chủ đặt 1 nậm rượu, khi cúng có thể để mở nắp nậm hoặc rót rượu ra chén.</p>

<p>+ Kỷ chén (đựng nước) và nậm rượu: Kỷ chén dùng để rót nước, rót rượu khi thờ cúng. Ban thờ có thể đặt kỷ 3 chén hoặc 5 chén ở chính giữa ban thờ hoặc ở một phía của ban thờ.</p>

<p>Đồ thờ cần có trên ban thờ truyền thống của người Việt</p>

<p>Những đồ thờ có thể đặt thêm trên ban thờ</p>

<p>Với những gia đình cầu kỳ hơn có thể đặt thêm các đồ thờ khác như:</p>

<p>+ Chóe thờ: Trong gia đình Việt ngày xưa, chóe chính là những chiếc chum lớn có nắp dùng để đựng gạo, đựng muối, đựng nước sạch. Với quan niệm trần sao thì âm vậy, nhiều gia đình đặt chóe muối, gạo, nước lên ban thờ để thờ cúng tổ tiên.</p>

<p>Các gia đình có thể đặt chóe lên ban thờ với số lượng từ 1 chóe đến 3 chóe, bên trong có thể đựng gạo, muối, nước sạch hoặc rượu, tiền. Nếu ban thờ nhỏ, gia chủ nên đặt 1 chóe sang 1 bên ban thờ. Đặt 2 chóe thì nên đặt đối xứng hai bên ban thờ. Bàn thờ kích thước lớn, gia chủ đặt 3 chóe cạnh nhau theo hình tam giác.</p>

<p>+ Bộ trà: Bộ trà cúng được đặt lên ban thờ với mục đích pha trà, dâng trà trong những ngày cúng lễ. Ban thờ có thể đặt thêm 1 bộ trà ở vị trí phía trước.</p>

<p>+ Bát sâm: Bát sâm hay còn gọi là bát trà sâm, chén trà sâm. Bát sâm có đế lót và có nắp đậy, dùng để dâng trà hoặc đựng nước trong ngày thờ cúng. Gia chủ có thể bày 1 bát sâm hoặc 2 bát sâm đối xứng nhau trên ban thờ.</p>

<p>+ Bát cúng, đũa thờ: Bát cúng, đũa thờ có thể bày trên ban thờ để tăng thêm sự đầy đủ, sung túc và tính uy nghi. Bát cúng, đũa thờ có thể bày theo bộ 6 bát 6 đũa, 10 bát 10 đũa. Bát cúng chia làm hai đặt sang 2 phía ban thờ. Đũa cúng chỉ cần đặt theo bộ đã thiết kế sẵn.</p>

<p>+ Ống hương: Ống hương đặt hương sẽ giúp ban thờ luôn gọn gàng, ngăn nắp. Ống hương có thể đặt sang một phía phan thờ, đối diện với nậm rượu.</p>

<p>+ Đèn dầu và chân nến: Trên ban thờ cần có đèn dầu, chân nến để thắp lửa, giữ lửa. Đèn dầu có thể đặt 1 đèn hoặc 2 đèn tùy vào kích thước ban thờ tại gia. Chân nến đặt cũng như vậy.</p>

<p>Đồ thờ đặt thêm giúp ban thờ gia tiên&nbsp;trở nên uy nghi đầy đủ</p>

<p>Ngoài các sản phẩm đồ thờ trên đây, nếu ban thờ tại gia kích thước rất lớn, gia chủ có thể đặt thêm các đồ thờ như:</p>

<p>+ Bộ tam sự: Chính là bộ đỉnh hạc gồm 1 lư hương và 2 con hạc. Bộ tam sự sẽ đặt chính giữa và bên trong cùng của ban thờ. Lư hương có thể đặt lên chân đế để tôn cao lên.</p>

<p>+ Bộ ngũ sự: Bao gồm bộ đỉnh hạc và 2 cây nến lớn. Bộ đỉnh hạc vẫn đặt như trên, 2 cây nến sẽ đặt phía ngoài cùng, đằng trước ban thờ và đối diện sang hai bên.</p>

<p>+ Mâm bồng: Ngoài 1 mâm bồng lớn ở chính giữa, ban thờ lớn có thể đặt thêm 2 mâm bồng nhỏ sang hai bên để đặt trầu cau, tiền vàng.</p>

<p>+ Lục bình: Gia chủ có thể đặt đôi lọ lộc bình lớn để cắm sen gỗ, cắm cành đào ngày Tết, 2 lọ lục bình nhỏ dùng để cắm hoa tươi. Đôi lộc bình sẽ đặt đối xứng hai bên ban thờ.</p>

<p><a id="Để có giá chuẩn nhất, quý khách hàng xin hãy liên hệ Hotline 0918.482.648 để nhận tư vấn trực tiếp." name="Để có giá chuẩn nhất, quý khách hàng xin hãy liên hệ Hotline 0918.482... để nhận tư vấn trực tiếp.">Để có giá chuẩn nhất, quý khách hàng xin hãy liên hệ Hotline 0918.482.648 để nhận tư vấn trực tiếp.</a></p>
', N'lam-sao-de-bai-tri-sap-xep-duoc-ban-tho-dung-y-vua-day-du-vua-hai-hoa-va-dong-bo-210131-2151441', N'', N'', N'', N'vi', 1, 1, 0, 1, 1, N'', CAST(N'2021-01-31T21:05:54.350' AS DateTime), CAST(N'2021-01-31T21:05:54.350' AS DateTime))
GO
INSERT [dbo].[News] ([id], [groupId], [newImage], [title], [descript], [contents], [new_key], [titleSeo], [keySeo], [desSeo], [newLang], [newOrder], [status], [viewCount], [newHot], [newNew], [newFile], [createDate], [updateDate]) VALUES (N'82d35237-45db-47dd-a706-7c3b9088ac39', N'a181dad0-69c1-472c-bb80-01ed0483791e', N'/uploads/news/c12024-300x300.jpg', N'LÀM SAO ĐỂ BÀI TRÍ, SẮP XẾP ĐƯỢC BAN THỜ ĐÚNG Ý, VỪA ĐẦY ĐỦ VỪA HÀI HÒA VÀ ĐỒNG BỘ', N'Làm sao để bài trí, sắp xếp được ban thờ đúng ý, vừa đầy đủ vừa hài hòa và đồng bộ Trên một ban thờ cần lựa chọn những đồ thờ nào, kích thước ra sao, cách bài trí như thế nào? Gia chủ hãy tham khảo những thông tin tư vấn dưới đây để hiểu hơn về cách lựa chọn và sắp xếp đồ thờ phù hợp. Hiện nay, bộ đồ thờ gia tiên Bát Tràng gồm những loại sau 1. Bộ đồ thờ men xanh lam cổ 2. Bộ đồ thờ men rạn cổ 3. Bộ đồ thờ đắp nổi men...', N'<h2>Trên một ban thờ cần lựa chọn những đồ thờ nào, kích thước ra sao, cách bài trí như thế nào? Gia chủ hãy tham khảo những thông tin tư vấn dưới đây để hiểu hơn về cách lựa chọn và sắp xếp đồ thờ phù hợp.</h2>

<p>Hiện nay, bộ đồ thờ gia tiên Bát Tràng gồm những loại sau</p>

<p>1. Bộ đồ thờ men xanh lam cổ</p>

<p>2. Bộ đồ thờ men rạn cổ</p>

<p>3. Bộ đồ thờ đắp nổi men rạn cao cấp</p>

<p>4. Bộ đồ thờ vẽ vàng kim cao cấp</p>

<p>5. Bộ đồ thờ xanh lục bảo bọc đồng cao cấp</p>

<p>6. Bộ đồ thờ men hoàng thổ cao cấp</p>

<p><a id="Những đồ thờ cần có trên ban thờ theo truyền thống, dựa vào kích thước ban thờ để lựa chọn bát hương cho phù hợp" name="Những đồ thờ cần có trên ban thờ theo truyền thống, dựa vào kích thước ban thờ để lựa chọn bát hương cho phù hợp">Những đồ thờ cần có trên ban thờ theo truyền thống, dựa vào kích thước ban thờ để lựa chọn bát hương cho phù hợp</a></p>

<p><font color="#e74c3c">Đối với ban thờ có kích thước dài 1m57, cao 1m27, quý khách nên chọn bát hương ở giữa phi 18cm, 2 bát hương còn lại phi 16cm</font></p>

<p><font color="#e74c3c">Đối với ban thờ có kích thước dài 1m97, 2m17, cao 1m27, quý khách nên chọn bát hương ở giữa phi 20cm, 2 bát hương còn lại phi 18cm</font></p>

<p>Theo truyền thống, một ban thờ cơ bản sẽ có những đồ thờ cơ bản đó là:</p>

<p>+ Bát hương: Nếu gia đình chỉ thờ thần linh, gia chủ đặt 1 bát hương trên bàn thờ. Gia đình thờ gia tiên sẽ đặt 3 bát hương, 1 bát hương cỡ lớn và 2 bát hương nhỏ</p>

<p>Khi đặt bát hương trên ban thờ, gia chủ có thể đặt trên đế bát hương bằng sứ, đế bát hương bằng gỗ hoặc đặt trên tam cấp cho đẹp.</p>

<p>+ Lộc bình (lọ hoa): Ngoài bát hương, trên ban thờ đặt thêm lọ hoa nhỏ để cắm hoa tươi. Gia chủ có thể đặt lọ hoa theo số lượng tùy ý ở 2 bên ban thờ.</p>

<p>+ Mâm bồng (Đĩa đựng lễ vật): Một ban thờ cơ bản sẽ có 1 mâm bồng đặt chính giữa ban thờ. Mâm bồng có thể đặt hoa quả, bánh kẹo, trầu cau, tiền vàng,&hellip;</p>

<p>+ Nậm rượu: Nậm đựng rượu cúng bằng gốm sứ. Bàn thờ nhỏ gia chủ đặt 1 nậm rượu, khi cúng có thể để mở nắp nậm hoặc rót rượu ra chén.</p>

<p>+ Kỷ chén (đựng nước) và nậm rượu: Kỷ chén dùng để rót nước, rót rượu khi thờ cúng. Ban thờ có thể đặt kỷ 3 chén hoặc 5 chén ở chính giữa ban thờ hoặc ở một phía của ban thờ.</p>

<p>Đồ thờ cần có trên ban thờ truyền thống của người Việt</p>

<p>Những đồ thờ có thể đặt thêm trên ban thờ</p>

<p>Với những gia đình cầu kỳ hơn có thể đặt thêm các đồ thờ khác như:</p>

<p>+ Chóe thờ: Trong gia đình Việt ngày xưa, chóe chính là những chiếc chum lớn có nắp dùng để đựng gạo, đựng muối, đựng nước sạch. Với quan niệm trần sao thì âm vậy, nhiều gia đình đặt chóe muối, gạo, nước lên ban thờ để thờ cúng tổ tiên.</p>

<p>Các gia đình có thể đặt chóe lên ban thờ với số lượng từ 1 chóe đến 3 chóe, bên trong có thể đựng gạo, muối, nước sạch hoặc rượu, tiền. Nếu ban thờ nhỏ, gia chủ nên đặt 1 chóe sang 1 bên ban thờ. Đặt 2 chóe thì nên đặt đối xứng hai bên ban thờ. Bàn thờ kích thước lớn, gia chủ đặt 3 chóe cạnh nhau theo hình tam giác.</p>

<p>+ Bộ trà: Bộ trà cúng được đặt lên ban thờ với mục đích pha trà, dâng trà trong những ngày cúng lễ. Ban thờ có thể đặt thêm 1 bộ trà ở vị trí phía trước.</p>

<p>+ Bát sâm: Bát sâm hay còn gọi là bát trà sâm, chén trà sâm. Bát sâm có đế lót và có nắp đậy, dùng để dâng trà hoặc đựng nước trong ngày thờ cúng. Gia chủ có thể bày 1 bát sâm hoặc 2 bát sâm đối xứng nhau trên ban thờ.</p>

<p>+ Bát cúng, đũa thờ: Bát cúng, đũa thờ có thể bày trên ban thờ để tăng thêm sự đầy đủ, sung túc và tính uy nghi. Bát cúng, đũa thờ có thể bày theo bộ 6 bát 6 đũa, 10 bát 10 đũa. Bát cúng chia làm hai đặt sang 2 phía ban thờ. Đũa cúng chỉ cần đặt theo bộ đã thiết kế sẵn.</p>

<p>+ Ống hương: Ống hương đặt hương sẽ giúp ban thờ luôn gọn gàng, ngăn nắp. Ống hương có thể đặt sang một phía phan thờ, đối diện với nậm rượu.</p>

<p>+ Đèn dầu và chân nến: Trên ban thờ cần có đèn dầu, chân nến để thắp lửa, giữ lửa. Đèn dầu có thể đặt 1 đèn hoặc 2 đèn tùy vào kích thước ban thờ tại gia. Chân nến đặt cũng như vậy.</p>

<p>Đồ thờ đặt thêm giúp ban thờ gia tiên&nbsp;trở nên uy nghi đầy đủ</p>

<p>Ngoài các sản phẩm đồ thờ trên đây, nếu ban thờ tại gia kích thước rất lớn, gia chủ có thể đặt thêm các đồ thờ như:</p>

<p>+ Bộ tam sự: Chính là bộ đỉnh hạc gồm 1 lư hương và 2 con hạc. Bộ tam sự sẽ đặt chính giữa và bên trong cùng của ban thờ. Lư hương có thể đặt lên chân đế để tôn cao lên.</p>

<p>+ Bộ ngũ sự: Bao gồm bộ đỉnh hạc và 2 cây nến lớn. Bộ đỉnh hạc vẫn đặt như trên, 2 cây nến sẽ đặt phía ngoài cùng, đằng trước ban thờ và đối diện sang hai bên.</p>

<p>+ Mâm bồng: Ngoài 1 mâm bồng lớn ở chính giữa, ban thờ lớn có thể đặt thêm 2 mâm bồng nhỏ sang hai bên để đặt trầu cau, tiền vàng.</p>

<p>+ Lục bình: Gia chủ có thể đặt đôi lọ lộc bình lớn để cắm sen gỗ, cắm cành đào ngày Tết, 2 lọ lục bình nhỏ dùng để cắm hoa tươi. Đôi lộc bình sẽ đặt đối xứng hai bên ban thờ.</p>

<p><a id="Để có giá chuẩn nhất, quý khách hàng xin hãy liên hệ Hotline 0918.482.648 để nhận tư vấn trực tiếp." name="Để có giá chuẩn nhất, quý khách hàng xin hãy liên hệ Hotline 0918.482... để nhận tư vấn trực tiếp.">Để có giá chuẩn nhất, quý khách hàng xin hãy liên hệ Hotline 0918.482.648 để nhận tư vấn trực tiếp.</a></p>
', N'lam-sao-de-bai-tri-sap-xep-duoc-ban-tho-dung-y-vua-day-du-vua-hai-hoa-va-dong-bo', N'', N'', N'', N'vi', 1, 1, 0, 1, 1, N'', CAST(N'2021-01-31T21:05:54.350' AS DateTime), CAST(N'2021-01-31T21:05:54.350' AS DateTime))
GO
INSERT [dbo].[News] ([id], [groupId], [newImage], [title], [descript], [contents], [new_key], [titleSeo], [keySeo], [desSeo], [newLang], [newOrder], [status], [viewCount], [newHot], [newNew], [newFile], [createDate], [updateDate]) VALUES (N'b3acb2ba-5ab0-4732-8781-f70d985bfb50', N'a181dad0-69c1-472c-bb80-01ed0483791e', N'/uploads/news/c12024-300x300.jpg', N'LÀM SAO ĐỂ BÀI TRÍ, SẮP XẾP ĐƯỢC BAN THỜ ĐÚNG Ý, VỪA ĐẦY ĐỦ VỪA HÀI HÒA VÀ ĐỒNG BỘ', N'Làm sao để bài trí, sắp xếp được ban thờ đúng ý, vừa đầy đủ vừa hài hòa và đồng bộ Trên một ban thờ cần lựa chọn những đồ thờ nào, kích thước ra sao, cách bài trí như thế nào? Gia chủ hãy tham khảo những thông tin tư vấn dưới đây để hiểu hơn về cách lựa chọn và sắp xếp đồ thờ phù hợp. Hiện nay, bộ đồ thờ gia tiên Bát Tràng gồm những loại sau 1. Bộ đồ thờ men xanh lam cổ 2. Bộ đồ thờ men rạn cổ 3. Bộ đồ thờ đắp nổi men...', N'<h2>Trên một ban thờ cần lựa chọn những đồ thờ nào, kích thước ra sao, cách bài trí như thế nào? Gia chủ hãy tham khảo những thông tin tư vấn dưới đây để hiểu hơn về cách lựa chọn và sắp xếp đồ thờ phù hợp.</h2>

<p>Hiện nay, bộ đồ thờ gia tiên Bát Tràng gồm những loại sau</p>

<p>1. Bộ đồ thờ men xanh lam cổ</p>

<p>2. Bộ đồ thờ men rạn cổ</p>

<p>3. Bộ đồ thờ đắp nổi men rạn cao cấp</p>

<p>4. Bộ đồ thờ vẽ vàng kim cao cấp</p>

<p>5. Bộ đồ thờ xanh lục bảo bọc đồng cao cấp</p>

<p>6. Bộ đồ thờ men hoàng thổ cao cấp</p>

<p><a id="Những đồ thờ cần có trên ban thờ theo truyền thống, dựa vào kích thước ban thờ để lựa chọn bát hương cho phù hợp" name="Những đồ thờ cần có trên ban thờ theo truyền thống, dựa vào kích thước ban thờ để lựa chọn bát hương cho phù hợp">Những đồ thờ cần có trên ban thờ theo truyền thống, dựa vào kích thước ban thờ để lựa chọn bát hương cho phù hợp</a></p>

<p><font color="#e74c3c">Đối với ban thờ có kích thước dài 1m57, cao 1m27, quý khách nên chọn bát hương ở giữa phi 18cm, 2 bát hương còn lại phi 16cm</font></p>

<p><font color="#e74c3c">Đối với ban thờ có kích thước dài 1m97, 2m17, cao 1m27, quý khách nên chọn bát hương ở giữa phi 20cm, 2 bát hương còn lại phi 18cm</font></p>

<p>Theo truyền thống, một ban thờ cơ bản sẽ có những đồ thờ cơ bản đó là:</p>

<p>+ Bát hương: Nếu gia đình chỉ thờ thần linh, gia chủ đặt 1 bát hương trên bàn thờ. Gia đình thờ gia tiên sẽ đặt 3 bát hương, 1 bát hương cỡ lớn và 2 bát hương nhỏ</p>

<p>Khi đặt bát hương trên ban thờ, gia chủ có thể đặt trên đế bát hương bằng sứ, đế bát hương bằng gỗ hoặc đặt trên tam cấp cho đẹp.</p>

<p>+ Lộc bình (lọ hoa): Ngoài bát hương, trên ban thờ đặt thêm lọ hoa nhỏ để cắm hoa tươi. Gia chủ có thể đặt lọ hoa theo số lượng tùy ý ở 2 bên ban thờ.</p>

<p>+ Mâm bồng (Đĩa đựng lễ vật): Một ban thờ cơ bản sẽ có 1 mâm bồng đặt chính giữa ban thờ. Mâm bồng có thể đặt hoa quả, bánh kẹo, trầu cau, tiền vàng,&hellip;</p>

<p>+ Nậm rượu: Nậm đựng rượu cúng bằng gốm sứ. Bàn thờ nhỏ gia chủ đặt 1 nậm rượu, khi cúng có thể để mở nắp nậm hoặc rót rượu ra chén.</p>

<p>+ Kỷ chén (đựng nước) và nậm rượu: Kỷ chén dùng để rót nước, rót rượu khi thờ cúng. Ban thờ có thể đặt kỷ 3 chén hoặc 5 chén ở chính giữa ban thờ hoặc ở một phía của ban thờ.</p>

<p>Đồ thờ cần có trên ban thờ truyền thống của người Việt</p>

<p>Những đồ thờ có thể đặt thêm trên ban thờ</p>

<p>Với những gia đình cầu kỳ hơn có thể đặt thêm các đồ thờ khác như:</p>

<p>+ Chóe thờ: Trong gia đình Việt ngày xưa, chóe chính là những chiếc chum lớn có nắp dùng để đựng gạo, đựng muối, đựng nước sạch. Với quan niệm trần sao thì âm vậy, nhiều gia đình đặt chóe muối, gạo, nước lên ban thờ để thờ cúng tổ tiên.</p>

<p>Các gia đình có thể đặt chóe lên ban thờ với số lượng từ 1 chóe đến 3 chóe, bên trong có thể đựng gạo, muối, nước sạch hoặc rượu, tiền. Nếu ban thờ nhỏ, gia chủ nên đặt 1 chóe sang 1 bên ban thờ. Đặt 2 chóe thì nên đặt đối xứng hai bên ban thờ. Bàn thờ kích thước lớn, gia chủ đặt 3 chóe cạnh nhau theo hình tam giác.</p>

<p>+ Bộ trà: Bộ trà cúng được đặt lên ban thờ với mục đích pha trà, dâng trà trong những ngày cúng lễ. Ban thờ có thể đặt thêm 1 bộ trà ở vị trí phía trước.</p>

<p>+ Bát sâm: Bát sâm hay còn gọi là bát trà sâm, chén trà sâm. Bát sâm có đế lót và có nắp đậy, dùng để dâng trà hoặc đựng nước trong ngày thờ cúng. Gia chủ có thể bày 1 bát sâm hoặc 2 bát sâm đối xứng nhau trên ban thờ.</p>

<p>+ Bát cúng, đũa thờ: Bát cúng, đũa thờ có thể bày trên ban thờ để tăng thêm sự đầy đủ, sung túc và tính uy nghi. Bát cúng, đũa thờ có thể bày theo bộ 6 bát 6 đũa, 10 bát 10 đũa. Bát cúng chia làm hai đặt sang 2 phía ban thờ. Đũa cúng chỉ cần đặt theo bộ đã thiết kế sẵn.</p>

<p>+ Ống hương: Ống hương đặt hương sẽ giúp ban thờ luôn gọn gàng, ngăn nắp. Ống hương có thể đặt sang một phía phan thờ, đối diện với nậm rượu.</p>

<p>+ Đèn dầu và chân nến: Trên ban thờ cần có đèn dầu, chân nến để thắp lửa, giữ lửa. Đèn dầu có thể đặt 1 đèn hoặc 2 đèn tùy vào kích thước ban thờ tại gia. Chân nến đặt cũng như vậy.</p>

<p>Đồ thờ đặt thêm giúp ban thờ gia tiên&nbsp;trở nên uy nghi đầy đủ</p>

<p>Ngoài các sản phẩm đồ thờ trên đây, nếu ban thờ tại gia kích thước rất lớn, gia chủ có thể đặt thêm các đồ thờ như:</p>

<p>+ Bộ tam sự: Chính là bộ đỉnh hạc gồm 1 lư hương và 2 con hạc. Bộ tam sự sẽ đặt chính giữa và bên trong cùng của ban thờ. Lư hương có thể đặt lên chân đế để tôn cao lên.</p>

<p>+ Bộ ngũ sự: Bao gồm bộ đỉnh hạc và 2 cây nến lớn. Bộ đỉnh hạc vẫn đặt như trên, 2 cây nến sẽ đặt phía ngoài cùng, đằng trước ban thờ và đối diện sang hai bên.</p>

<p>+ Mâm bồng: Ngoài 1 mâm bồng lớn ở chính giữa, ban thờ lớn có thể đặt thêm 2 mâm bồng nhỏ sang hai bên để đặt trầu cau, tiền vàng.</p>

<p>+ Lục bình: Gia chủ có thể đặt đôi lọ lộc bình lớn để cắm sen gỗ, cắm cành đào ngày Tết, 2 lọ lục bình nhỏ dùng để cắm hoa tươi. Đôi lộc bình sẽ đặt đối xứng hai bên ban thờ.</p>

<p><a id="Để có giá chuẩn nhất, quý khách hàng xin hãy liên hệ Hotline 0918.482.648 để nhận tư vấn trực tiếp." name="Để có giá chuẩn nhất, quý khách hàng xin hãy liên hệ Hotline 0918.482... để nhận tư vấn trực tiếp.">Để có giá chuẩn nhất, quý khách hàng xin hãy liên hệ Hotline 0918.482.648 để nhận tư vấn trực tiếp.</a></p>
', N'lam-sao-de-bai-tri-sap-xep-duoc-ban-tho-dung-y-vua-day-du-vua-hai-hoa-va-dong-bo-210131-2151444', N'', N'', N'', N'vi', 1, 1, 0, 1, 1, N'', CAST(N'2021-01-31T21:05:54.350' AS DateTime), CAST(N'2021-01-31T21:05:54.350' AS DateTime))
GO
INSERT [dbo].[News] ([id], [groupId], [newImage], [title], [descript], [contents], [new_key], [titleSeo], [keySeo], [desSeo], [newLang], [newOrder], [status], [viewCount], [newHot], [newNew], [newFile], [createDate], [updateDate]) VALUES (N'b98488f3-9d13-4d84-8772-46b8d75d0f49', N'a181dad0-69c1-472c-bb80-01ed0483791e', N'/uploads/news/c12024-300x300.jpg', N'LÀM SAO ĐỂ BÀI TRÍ, SẮP XẾP ĐƯỢC BAN THỜ ĐÚNG Ý, VỪA ĐẦY ĐỦ VỪA HÀI HÒA VÀ ĐỒNG BỘ', N'Làm sao để bài trí, sắp xếp được ban thờ đúng ý, vừa đầy đủ vừa hài hòa và đồng bộ Trên một ban thờ cần lựa chọn những đồ thờ nào, kích thước ra sao, cách bài trí như thế nào? Gia chủ hãy tham khảo những thông tin tư vấn dưới đây để hiểu hơn về cách lựa chọn và sắp xếp đồ thờ phù hợp. Hiện nay, bộ đồ thờ gia tiên Bát Tràng gồm những loại sau 1. Bộ đồ thờ men xanh lam cổ 2. Bộ đồ thờ men rạn cổ 3. Bộ đồ thờ đắp nổi men...', N'<h2>Trên một ban thờ cần lựa chọn những đồ thờ nào, kích thước ra sao, cách bài trí như thế nào? Gia chủ hãy tham khảo những thông tin tư vấn dưới đây để hiểu hơn về cách lựa chọn và sắp xếp đồ thờ phù hợp.</h2>

<p>Hiện nay, bộ đồ thờ gia tiên Bát Tràng gồm những loại sau</p>

<p>1. Bộ đồ thờ men xanh lam cổ</p>

<p>2. Bộ đồ thờ men rạn cổ</p>

<p>3. Bộ đồ thờ đắp nổi men rạn cao cấp</p>

<p>4. Bộ đồ thờ vẽ vàng kim cao cấp</p>

<p>5. Bộ đồ thờ xanh lục bảo bọc đồng cao cấp</p>

<p>6. Bộ đồ thờ men hoàng thổ cao cấp</p>

<p><a id="Những đồ thờ cần có trên ban thờ theo truyền thống, dựa vào kích thước ban thờ để lựa chọn bát hương cho phù hợp" name="Những đồ thờ cần có trên ban thờ theo truyền thống, dựa vào kích thước ban thờ để lựa chọn bát hương cho phù hợp">Những đồ thờ cần có trên ban thờ theo truyền thống, dựa vào kích thước ban thờ để lựa chọn bát hương cho phù hợp</a></p>

<p><font color="#e74c3c">Đối với ban thờ có kích thước dài 1m57, cao 1m27, quý khách nên chọn bát hương ở giữa phi 18cm, 2 bát hương còn lại phi 16cm</font></p>

<p><font color="#e74c3c">Đối với ban thờ có kích thước dài 1m97, 2m17, cao 1m27, quý khách nên chọn bát hương ở giữa phi 20cm, 2 bát hương còn lại phi 18cm</font></p>

<p>Theo truyền thống, một ban thờ cơ bản sẽ có những đồ thờ cơ bản đó là:</p>

<p>+ Bát hương: Nếu gia đình chỉ thờ thần linh, gia chủ đặt 1 bát hương trên bàn thờ. Gia đình thờ gia tiên sẽ đặt 3 bát hương, 1 bát hương cỡ lớn và 2 bát hương nhỏ</p>

<p>Khi đặt bát hương trên ban thờ, gia chủ có thể đặt trên đế bát hương bằng sứ, đế bát hương bằng gỗ hoặc đặt trên tam cấp cho đẹp.</p>

<p>+ Lộc bình (lọ hoa): Ngoài bát hương, trên ban thờ đặt thêm lọ hoa nhỏ để cắm hoa tươi. Gia chủ có thể đặt lọ hoa theo số lượng tùy ý ở 2 bên ban thờ.</p>

<p>+ Mâm bồng (Đĩa đựng lễ vật): Một ban thờ cơ bản sẽ có 1 mâm bồng đặt chính giữa ban thờ. Mâm bồng có thể đặt hoa quả, bánh kẹo, trầu cau, tiền vàng,&hellip;</p>

<p>+ Nậm rượu: Nậm đựng rượu cúng bằng gốm sứ. Bàn thờ nhỏ gia chủ đặt 1 nậm rượu, khi cúng có thể để mở nắp nậm hoặc rót rượu ra chén.</p>

<p>+ Kỷ chén (đựng nước) và nậm rượu: Kỷ chén dùng để rót nước, rót rượu khi thờ cúng. Ban thờ có thể đặt kỷ 3 chén hoặc 5 chén ở chính giữa ban thờ hoặc ở một phía của ban thờ.</p>

<p>Đồ thờ cần có trên ban thờ truyền thống của người Việt</p>

<p>Những đồ thờ có thể đặt thêm trên ban thờ</p>

<p>Với những gia đình cầu kỳ hơn có thể đặt thêm các đồ thờ khác như:</p>

<p>+ Chóe thờ: Trong gia đình Việt ngày xưa, chóe chính là những chiếc chum lớn có nắp dùng để đựng gạo, đựng muối, đựng nước sạch. Với quan niệm trần sao thì âm vậy, nhiều gia đình đặt chóe muối, gạo, nước lên ban thờ để thờ cúng tổ tiên.</p>

<p>Các gia đình có thể đặt chóe lên ban thờ với số lượng từ 1 chóe đến 3 chóe, bên trong có thể đựng gạo, muối, nước sạch hoặc rượu, tiền. Nếu ban thờ nhỏ, gia chủ nên đặt 1 chóe sang 1 bên ban thờ. Đặt 2 chóe thì nên đặt đối xứng hai bên ban thờ. Bàn thờ kích thước lớn, gia chủ đặt 3 chóe cạnh nhau theo hình tam giác.</p>

<p>+ Bộ trà: Bộ trà cúng được đặt lên ban thờ với mục đích pha trà, dâng trà trong những ngày cúng lễ. Ban thờ có thể đặt thêm 1 bộ trà ở vị trí phía trước.</p>

<p>+ Bát sâm: Bát sâm hay còn gọi là bát trà sâm, chén trà sâm. Bát sâm có đế lót và có nắp đậy, dùng để dâng trà hoặc đựng nước trong ngày thờ cúng. Gia chủ có thể bày 1 bát sâm hoặc 2 bát sâm đối xứng nhau trên ban thờ.</p>

<p>+ Bát cúng, đũa thờ: Bát cúng, đũa thờ có thể bày trên ban thờ để tăng thêm sự đầy đủ, sung túc và tính uy nghi. Bát cúng, đũa thờ có thể bày theo bộ 6 bát 6 đũa, 10 bát 10 đũa. Bát cúng chia làm hai đặt sang 2 phía ban thờ. Đũa cúng chỉ cần đặt theo bộ đã thiết kế sẵn.</p>

<p>+ Ống hương: Ống hương đặt hương sẽ giúp ban thờ luôn gọn gàng, ngăn nắp. Ống hương có thể đặt sang một phía phan thờ, đối diện với nậm rượu.</p>

<p>+ Đèn dầu và chân nến: Trên ban thờ cần có đèn dầu, chân nến để thắp lửa, giữ lửa. Đèn dầu có thể đặt 1 đèn hoặc 2 đèn tùy vào kích thước ban thờ tại gia. Chân nến đặt cũng như vậy.</p>

<p>Đồ thờ đặt thêm giúp ban thờ gia tiên&nbsp;trở nên uy nghi đầy đủ</p>

<p>Ngoài các sản phẩm đồ thờ trên đây, nếu ban thờ tại gia kích thước rất lớn, gia chủ có thể đặt thêm các đồ thờ như:</p>

<p>+ Bộ tam sự: Chính là bộ đỉnh hạc gồm 1 lư hương và 2 con hạc. Bộ tam sự sẽ đặt chính giữa và bên trong cùng của ban thờ. Lư hương có thể đặt lên chân đế để tôn cao lên.</p>

<p>+ Bộ ngũ sự: Bao gồm bộ đỉnh hạc và 2 cây nến lớn. Bộ đỉnh hạc vẫn đặt như trên, 2 cây nến sẽ đặt phía ngoài cùng, đằng trước ban thờ và đối diện sang hai bên.</p>

<p>+ Mâm bồng: Ngoài 1 mâm bồng lớn ở chính giữa, ban thờ lớn có thể đặt thêm 2 mâm bồng nhỏ sang hai bên để đặt trầu cau, tiền vàng.</p>

<p>+ Lục bình: Gia chủ có thể đặt đôi lọ lộc bình lớn để cắm sen gỗ, cắm cành đào ngày Tết, 2 lọ lục bình nhỏ dùng để cắm hoa tươi. Đôi lộc bình sẽ đặt đối xứng hai bên ban thờ.</p>

<p><a id="Để có giá chuẩn nhất, quý khách hàng xin hãy liên hệ Hotline 0918.482.648 để nhận tư vấn trực tiếp." name="Để có giá chuẩn nhất, quý khách hàng xin hãy liên hệ Hotline 0918.482... để nhận tư vấn trực tiếp.">Để có giá chuẩn nhất, quý khách hàng xin hãy liên hệ Hotline 0918.482.648 để nhận tư vấn trực tiếp.</a></p>
', N'lam-sao-de-bai-tri-sap-xep-duoc-ban-tho-dung-y-vua-day-du-vua-hai-hoa-va-dong-bo-210131-2151443', N'', N'', N'', N'vi', 1, 1, 0, 1, 1, N'', CAST(N'2021-01-31T21:05:54.350' AS DateTime), CAST(N'2021-01-31T21:05:54.350' AS DateTime))
GO
INSERT [dbo].[News] ([id], [groupId], [newImage], [title], [descript], [contents], [new_key], [titleSeo], [keySeo], [desSeo], [newLang], [newOrder], [status], [viewCount], [newHot], [newNew], [newFile], [createDate], [updateDate]) VALUES (N'e1c6269c-25a1-4787-8e41-aea305e878d8', N'a181dad0-69c1-472c-bb80-01ed0483791e', N'/uploads/news/c12024-300x300.jpg', N'LÀM SAO ĐỂ BÀI TRÍ, SẮP XẾP ĐƯỢC BAN THỜ ĐÚNG Ý, VỪA ĐẦY ĐỦ VỪA HÀI HÒA VÀ ĐỒNG BỘ', N'Làm sao để bài trí, sắp xếp được ban thờ đúng ý, vừa đầy đủ vừa hài hòa và đồng bộ Trên một ban thờ cần lựa chọn những đồ thờ nào, kích thước ra sao, cách bài trí như thế nào? Gia chủ hãy tham khảo những thông tin tư vấn dưới đây để hiểu hơn về cách lựa chọn và sắp xếp đồ thờ phù hợp. Hiện nay, bộ đồ thờ gia tiên Bát Tràng gồm những loại sau 1. Bộ đồ thờ men xanh lam cổ 2. Bộ đồ thờ men rạn cổ 3. Bộ đồ thờ đắp nổi men...', N'<h2>Trên một ban thờ cần lựa chọn những đồ thờ nào, kích thước ra sao, cách bài trí như thế nào? Gia chủ hãy tham khảo những thông tin tư vấn dưới đây để hiểu hơn về cách lựa chọn và sắp xếp đồ thờ phù hợp.</h2>

<p>Hiện nay, bộ đồ thờ gia tiên Bát Tràng gồm những loại sau</p>

<p>1. Bộ đồ thờ men xanh lam cổ</p>

<p>2. Bộ đồ thờ men rạn cổ</p>

<p>3. Bộ đồ thờ đắp nổi men rạn cao cấp</p>

<p>4. Bộ đồ thờ vẽ vàng kim cao cấp</p>

<p>5. Bộ đồ thờ xanh lục bảo bọc đồng cao cấp</p>

<p>6. Bộ đồ thờ men hoàng thổ cao cấp</p>

<p><a id="Những đồ thờ cần có trên ban thờ theo truyền thống, dựa vào kích thước ban thờ để lựa chọn bát hương cho phù hợp" name="Những đồ thờ cần có trên ban thờ theo truyền thống, dựa vào kích thước ban thờ để lựa chọn bát hương cho phù hợp">Những đồ thờ cần có trên ban thờ theo truyền thống, dựa vào kích thước ban thờ để lựa chọn bát hương cho phù hợp</a></p>

<p><font color="#e74c3c">Đối với ban thờ có kích thước dài 1m57, cao 1m27, quý khách nên chọn bát hương ở giữa phi 18cm, 2 bát hương còn lại phi 16cm</font></p>

<p><font color="#e74c3c">Đối với ban thờ có kích thước dài 1m97, 2m17, cao 1m27, quý khách nên chọn bát hương ở giữa phi 20cm, 2 bát hương còn lại phi 18cm</font></p>

<p>Theo truyền thống, một ban thờ cơ bản sẽ có những đồ thờ cơ bản đó là:</p>

<p>+ Bát hương: Nếu gia đình chỉ thờ thần linh, gia chủ đặt 1 bát hương trên bàn thờ. Gia đình thờ gia tiên sẽ đặt 3 bát hương, 1 bát hương cỡ lớn và 2 bát hương nhỏ</p>

<p>Khi đặt bát hương trên ban thờ, gia chủ có thể đặt trên đế bát hương bằng sứ, đế bát hương bằng gỗ hoặc đặt trên tam cấp cho đẹp.</p>

<p>+ Lộc bình (lọ hoa): Ngoài bát hương, trên ban thờ đặt thêm lọ hoa nhỏ để cắm hoa tươi. Gia chủ có thể đặt lọ hoa theo số lượng tùy ý ở 2 bên ban thờ.</p>

<p>+ Mâm bồng (Đĩa đựng lễ vật): Một ban thờ cơ bản sẽ có 1 mâm bồng đặt chính giữa ban thờ. Mâm bồng có thể đặt hoa quả, bánh kẹo, trầu cau, tiền vàng,&hellip;</p>

<p>+ Nậm rượu: Nậm đựng rượu cúng bằng gốm sứ. Bàn thờ nhỏ gia chủ đặt 1 nậm rượu, khi cúng có thể để mở nắp nậm hoặc rót rượu ra chén.</p>

<p>+ Kỷ chén (đựng nước) và nậm rượu: Kỷ chén dùng để rót nước, rót rượu khi thờ cúng. Ban thờ có thể đặt kỷ 3 chén hoặc 5 chén ở chính giữa ban thờ hoặc ở một phía của ban thờ.</p>

<p>Đồ thờ cần có trên ban thờ truyền thống của người Việt</p>

<p>Những đồ thờ có thể đặt thêm trên ban thờ</p>

<p>Với những gia đình cầu kỳ hơn có thể đặt thêm các đồ thờ khác như:</p>

<p>+ Chóe thờ: Trong gia đình Việt ngày xưa, chóe chính là những chiếc chum lớn có nắp dùng để đựng gạo, đựng muối, đựng nước sạch. Với quan niệm trần sao thì âm vậy, nhiều gia đình đặt chóe muối, gạo, nước lên ban thờ để thờ cúng tổ tiên.</p>

<p>Các gia đình có thể đặt chóe lên ban thờ với số lượng từ 1 chóe đến 3 chóe, bên trong có thể đựng gạo, muối, nước sạch hoặc rượu, tiền. Nếu ban thờ nhỏ, gia chủ nên đặt 1 chóe sang 1 bên ban thờ. Đặt 2 chóe thì nên đặt đối xứng hai bên ban thờ. Bàn thờ kích thước lớn, gia chủ đặt 3 chóe cạnh nhau theo hình tam giác.</p>

<p>+ Bộ trà: Bộ trà cúng được đặt lên ban thờ với mục đích pha trà, dâng trà trong những ngày cúng lễ. Ban thờ có thể đặt thêm 1 bộ trà ở vị trí phía trước.</p>

<p>+ Bát sâm: Bát sâm hay còn gọi là bát trà sâm, chén trà sâm. Bát sâm có đế lót và có nắp đậy, dùng để dâng trà hoặc đựng nước trong ngày thờ cúng. Gia chủ có thể bày 1 bát sâm hoặc 2 bát sâm đối xứng nhau trên ban thờ.</p>

<p>+ Bát cúng, đũa thờ: Bát cúng, đũa thờ có thể bày trên ban thờ để tăng thêm sự đầy đủ, sung túc và tính uy nghi. Bát cúng, đũa thờ có thể bày theo bộ 6 bát 6 đũa, 10 bát 10 đũa. Bát cúng chia làm hai đặt sang 2 phía ban thờ. Đũa cúng chỉ cần đặt theo bộ đã thiết kế sẵn.</p>

<p>+ Ống hương: Ống hương đặt hương sẽ giúp ban thờ luôn gọn gàng, ngăn nắp. Ống hương có thể đặt sang một phía phan thờ, đối diện với nậm rượu.</p>

<p>+ Đèn dầu và chân nến: Trên ban thờ cần có đèn dầu, chân nến để thắp lửa, giữ lửa. Đèn dầu có thể đặt 1 đèn hoặc 2 đèn tùy vào kích thước ban thờ tại gia. Chân nến đặt cũng như vậy.</p>

<p>Đồ thờ đặt thêm giúp ban thờ gia tiên&nbsp;trở nên uy nghi đầy đủ</p>

<p>Ngoài các sản phẩm đồ thờ trên đây, nếu ban thờ tại gia kích thước rất lớn, gia chủ có thể đặt thêm các đồ thờ như:</p>

<p>+ Bộ tam sự: Chính là bộ đỉnh hạc gồm 1 lư hương và 2 con hạc. Bộ tam sự sẽ đặt chính giữa và bên trong cùng của ban thờ. Lư hương có thể đặt lên chân đế để tôn cao lên.</p>

<p>+ Bộ ngũ sự: Bao gồm bộ đỉnh hạc và 2 cây nến lớn. Bộ đỉnh hạc vẫn đặt như trên, 2 cây nến sẽ đặt phía ngoài cùng, đằng trước ban thờ và đối diện sang hai bên.</p>

<p>+ Mâm bồng: Ngoài 1 mâm bồng lớn ở chính giữa, ban thờ lớn có thể đặt thêm 2 mâm bồng nhỏ sang hai bên để đặt trầu cau, tiền vàng.</p>

<p>+ Lục bình: Gia chủ có thể đặt đôi lọ lộc bình lớn để cắm sen gỗ, cắm cành đào ngày Tết, 2 lọ lục bình nhỏ dùng để cắm hoa tươi. Đôi lộc bình sẽ đặt đối xứng hai bên ban thờ.</p>

<p><a id="Để có giá chuẩn nhất, quý khách hàng xin hãy liên hệ Hotline 0918.482.648 để nhận tư vấn trực tiếp." name="Để có giá chuẩn nhất, quý khách hàng xin hãy liên hệ Hotline 0918.482... để nhận tư vấn trực tiếp.">Để có giá chuẩn nhất, quý khách hàng xin hãy liên hệ Hotline 0918.482.648 để nhận tư vấn trực tiếp.</a></p>
', N'lam-sao-de-bai-tri-sap-xep-duoc-ban-tho-dung-y-vua-day-du-vua-hai-hoa-va-dong-bo-210131-2151442', N'', N'', N'', N'vi', 1, 1, 0, 1, 1, N'', CAST(N'2021-01-31T21:05:54.350' AS DateTime), CAST(N'2021-01-31T21:05:54.350' AS DateTime))
GO
INSERT [dbo].[News] ([id], [groupId], [newImage], [title], [descript], [contents], [new_key], [titleSeo], [keySeo], [desSeo], [newLang], [newOrder], [status], [viewCount], [newHot], [newNew], [newFile], [createDate], [updateDate]) VALUES (N'ed25d188-a8ed-4b26-bd68-c0cce11467d0', N'a181dad0-69c1-472c-bb80-01ed0483791e', N'/uploads/news/c12024-300x300.jpg', N'LÀM SAO ĐỂ BÀI TRÍ, SẮP XẾP ĐƯỢC BAN THỜ ĐÚNG Ý, VỪA ĐẦY ĐỦ VỪA HÀI HÒA VÀ ĐỒNG BỘ', N'Làm sao để bài trí, sắp xếp được ban thờ đúng ý, vừa đầy đủ vừa hài hòa và đồng bộ Trên một ban thờ cần lựa chọn những đồ thờ nào, kích thước ra sao, cách bài trí như thế nào? Gia chủ hãy tham khảo những thông tin tư vấn dưới đây để hiểu hơn về cách lựa chọn và sắp xếp đồ thờ phù hợp. Hiện nay, bộ đồ thờ gia tiên Bát Tràng gồm những loại sau 1. Bộ đồ thờ men xanh lam cổ 2. Bộ đồ thờ men rạn cổ 3. Bộ đồ thờ đắp nổi men...', N'<h2>Trên một ban thờ cần lựa chọn những đồ thờ nào, kích thước ra sao, cách bài trí như thế nào? Gia chủ hãy tham khảo những thông tin tư vấn dưới đây để hiểu hơn về cách lựa chọn và sắp xếp đồ thờ phù hợp.</h2>

<p>Hiện nay, bộ đồ thờ gia tiên Bát Tràng gồm những loại sau</p>

<p>1. Bộ đồ thờ men xanh lam cổ</p>

<p>2. Bộ đồ thờ men rạn cổ</p>

<p>3. Bộ đồ thờ đắp nổi men rạn cao cấp</p>

<p>4. Bộ đồ thờ vẽ vàng kim cao cấp</p>

<p>5. Bộ đồ thờ xanh lục bảo bọc đồng cao cấp</p>

<p>6. Bộ đồ thờ men hoàng thổ cao cấp</p>

<p><a id="Những đồ thờ cần có trên ban thờ theo truyền thống, dựa vào kích thước ban thờ để lựa chọn bát hương cho phù hợp" name="Những đồ thờ cần có trên ban thờ theo truyền thống, dựa vào kích thước ban thờ để lựa chọn bát hương cho phù hợp">Những đồ thờ cần có trên ban thờ theo truyền thống, dựa vào kích thước ban thờ để lựa chọn bát hương cho phù hợp</a></p>

<p><font color="#e74c3c">Đối với ban thờ có kích thước dài 1m57, cao 1m27, quý khách nên chọn bát hương ở giữa phi 18cm, 2 bát hương còn lại phi 16cm</font></p>

<p><font color="#e74c3c">Đối với ban thờ có kích thước dài 1m97, 2m17, cao 1m27, quý khách nên chọn bát hương ở giữa phi 20cm, 2 bát hương còn lại phi 18cm</font></p>

<p>Theo truyền thống, một ban thờ cơ bản sẽ có những đồ thờ cơ bản đó là:</p>

<p>+ Bát hương: Nếu gia đình chỉ thờ thần linh, gia chủ đặt 1 bát hương trên bàn thờ. Gia đình thờ gia tiên sẽ đặt 3 bát hương, 1 bát hương cỡ lớn và 2 bát hương nhỏ</p>

<p>Khi đặt bát hương trên ban thờ, gia chủ có thể đặt trên đế bát hương bằng sứ, đế bát hương bằng gỗ hoặc đặt trên tam cấp cho đẹp.</p>

<p>+ Lộc bình (lọ hoa): Ngoài bát hương, trên ban thờ đặt thêm lọ hoa nhỏ để cắm hoa tươi. Gia chủ có thể đặt lọ hoa theo số lượng tùy ý ở 2 bên ban thờ.</p>

<p>+ Mâm bồng (Đĩa đựng lễ vật): Một ban thờ cơ bản sẽ có 1 mâm bồng đặt chính giữa ban thờ. Mâm bồng có thể đặt hoa quả, bánh kẹo, trầu cau, tiền vàng,&hellip;</p>

<p>+ Nậm rượu: Nậm đựng rượu cúng bằng gốm sứ. Bàn thờ nhỏ gia chủ đặt 1 nậm rượu, khi cúng có thể để mở nắp nậm hoặc rót rượu ra chén.</p>

<p>+ Kỷ chén (đựng nước) và nậm rượu: Kỷ chén dùng để rót nước, rót rượu khi thờ cúng. Ban thờ có thể đặt kỷ 3 chén hoặc 5 chén ở chính giữa ban thờ hoặc ở một phía của ban thờ.</p>

<p>Đồ thờ cần có trên ban thờ truyền thống của người Việt</p>

<p>Những đồ thờ có thể đặt thêm trên ban thờ</p>

<p>Với những gia đình cầu kỳ hơn có thể đặt thêm các đồ thờ khác như:</p>

<p>+ Chóe thờ: Trong gia đình Việt ngày xưa, chóe chính là những chiếc chum lớn có nắp dùng để đựng gạo, đựng muối, đựng nước sạch. Với quan niệm trần sao thì âm vậy, nhiều gia đình đặt chóe muối, gạo, nước lên ban thờ để thờ cúng tổ tiên.</p>

<p>Các gia đình có thể đặt chóe lên ban thờ với số lượng từ 1 chóe đến 3 chóe, bên trong có thể đựng gạo, muối, nước sạch hoặc rượu, tiền. Nếu ban thờ nhỏ, gia chủ nên đặt 1 chóe sang 1 bên ban thờ. Đặt 2 chóe thì nên đặt đối xứng hai bên ban thờ. Bàn thờ kích thước lớn, gia chủ đặt 3 chóe cạnh nhau theo hình tam giác.</p>

<p>+ Bộ trà: Bộ trà cúng được đặt lên ban thờ với mục đích pha trà, dâng trà trong những ngày cúng lễ. Ban thờ có thể đặt thêm 1 bộ trà ở vị trí phía trước.</p>

<p>+ Bát sâm: Bát sâm hay còn gọi là bát trà sâm, chén trà sâm. Bát sâm có đế lót và có nắp đậy, dùng để dâng trà hoặc đựng nước trong ngày thờ cúng. Gia chủ có thể bày 1 bát sâm hoặc 2 bát sâm đối xứng nhau trên ban thờ.</p>

<p>+ Bát cúng, đũa thờ: Bát cúng, đũa thờ có thể bày trên ban thờ để tăng thêm sự đầy đủ, sung túc và tính uy nghi. Bát cúng, đũa thờ có thể bày theo bộ 6 bát 6 đũa, 10 bát 10 đũa. Bát cúng chia làm hai đặt sang 2 phía ban thờ. Đũa cúng chỉ cần đặt theo bộ đã thiết kế sẵn.</p>

<p>+ Ống hương: Ống hương đặt hương sẽ giúp ban thờ luôn gọn gàng, ngăn nắp. Ống hương có thể đặt sang một phía phan thờ, đối diện với nậm rượu.</p>

<p>+ Đèn dầu và chân nến: Trên ban thờ cần có đèn dầu, chân nến để thắp lửa, giữ lửa. Đèn dầu có thể đặt 1 đèn hoặc 2 đèn tùy vào kích thước ban thờ tại gia. Chân nến đặt cũng như vậy.</p>

<p>Đồ thờ đặt thêm giúp ban thờ gia tiên&nbsp;trở nên uy nghi đầy đủ</p>

<p>Ngoài các sản phẩm đồ thờ trên đây, nếu ban thờ tại gia kích thước rất lớn, gia chủ có thể đặt thêm các đồ thờ như:</p>

<p>+ Bộ tam sự: Chính là bộ đỉnh hạc gồm 1 lư hương và 2 con hạc. Bộ tam sự sẽ đặt chính giữa và bên trong cùng của ban thờ. Lư hương có thể đặt lên chân đế để tôn cao lên.</p>

<p>+ Bộ ngũ sự: Bao gồm bộ đỉnh hạc và 2 cây nến lớn. Bộ đỉnh hạc vẫn đặt như trên, 2 cây nến sẽ đặt phía ngoài cùng, đằng trước ban thờ và đối diện sang hai bên.</p>

<p>+ Mâm bồng: Ngoài 1 mâm bồng lớn ở chính giữa, ban thờ lớn có thể đặt thêm 2 mâm bồng nhỏ sang hai bên để đặt trầu cau, tiền vàng.</p>

<p>+ Lục bình: Gia chủ có thể đặt đôi lọ lộc bình lớn để cắm sen gỗ, cắm cành đào ngày Tết, 2 lọ lục bình nhỏ dùng để cắm hoa tươi. Đôi lộc bình sẽ đặt đối xứng hai bên ban thờ.</p>

<p><a id="Để có giá chuẩn nhất, quý khách hàng xin hãy liên hệ Hotline 0918.482.648 để nhận tư vấn trực tiếp." name="Để có giá chuẩn nhất, quý khách hàng xin hãy liên hệ Hotline 0918.482... để nhận tư vấn trực tiếp.">Để có giá chuẩn nhất, quý khách hàng xin hãy liên hệ Hotline 0918.482.648 để nhận tư vấn trực tiếp.</a></p>
', N'lam-sao-de-bai-tri-sap-xep-duoc-ban-tho-dung-y-vua-day-du-vua-hai-hoa-va-dong-bo-210131-2151445', N'', N'', N'', N'vi', 1, 1, 0, 1, 1, N'', CAST(N'2021-01-31T21:05:54.350' AS DateTime), CAST(N'2021-01-31T21:05:54.350' AS DateTime))
GO
INSERT [dbo].[News] ([id], [groupId], [newImage], [title], [descript], [contents], [new_key], [titleSeo], [keySeo], [desSeo], [newLang], [newOrder], [status], [viewCount], [newHot], [newNew], [newFile], [createDate], [updateDate]) VALUES (N'f4c78177-9e5d-4565-a90b-c6ac49bb84f7', N'a181dad0-69c1-472c-bb80-01ed0483791e', N'/uploads/files/Untitled.png', N'Bộ bình ấm rượu tỳ bà - DL: 0,8L', N'Làm sao để bài trí, sắp xếp được ban thờ đúng ý, vừa đầy đủ vừa hài hòa và đồng bộ Trên một ban thờ cần lựa chọn những đồ thờ nào, kích thước ra sao, cách bài trí như thế nào? Gia chủ hãy tham khảo những thông tin tư vấn dưới đây để hiểu hơn về cách lựa chọn và sắp xếp đồ thờ phù hợp. Hiện nay, bộ đồ thờ gia tiên Bát Tràng gồm những loại sau 1. Bộ đồ thờ men xanh lam cổ 2. Bộ đồ thờ men rạn cổ 3. Bộ đồ thờ đắp nổi men...', N'<h2>Trên một ban thờ cần lựa chọn những đồ thờ nào, kích thước ra sao, cách bài trí như thế nào? Gia chủ hãy tham khảo những thông tin tư vấn dưới đây để hiểu hơn về cách lựa chọn và sắp xếp đồ thờ phù hợp.</h2>

<p>Hiện nay, bộ đồ thờ gia tiên Bát Tràng gồm những loại sau</p>

<p>1. Bộ đồ thờ men xanh lam cổ</p>

<p>2. Bộ đồ thờ men rạn cổ</p>

<p>3. Bộ đồ thờ đắp nổi men rạn cao cấp</p>

<p>4. Bộ đồ thờ vẽ vàng kim cao cấp</p>

<p>5. Bộ đồ thờ xanh lục bảo bọc đồng cao cấp</p>

<p>6. Bộ đồ thờ men hoàng thổ cao cấp</p>

<p><a id="Những đồ thờ cần có trên ban thờ theo truyền thống, dựa vào kích thước ban thờ để lựa chọn bát hương cho phù hợp" name="Những đồ thờ cần có trên ban thờ theo truyền thống, dựa vào kích thước ban thờ để lựa chọn bát hương cho phù hợp">Những đồ thờ cần có trên ban thờ theo truyền thống, dựa vào kích thước ban thờ để lựa chọn bát hương cho phù hợp</a></p>

<p><font color="#e74c3c">Đối với ban thờ có kích thước dài 1m57, cao 1m27, quý khách nên chọn bát hương ở giữa phi 18cm, 2 bát hương còn lại phi 16cm</font></p>

<p><font color="#e74c3c">Đối với ban thờ có kích thước dài 1m97, 2m17, cao 1m27, quý khách nên chọn bát hương ở giữa phi 20cm, 2 bát hương còn lại phi 18cm</font></p>

<p>Theo truyền thống, một ban thờ cơ bản sẽ có những đồ thờ cơ bản đó là:</p>

<p>+ Bát hương: Nếu gia đình chỉ thờ thần linh, gia chủ đặt 1 bát hương trên bàn thờ. Gia đình thờ gia tiên sẽ đặt 3 bát hương, 1 bát hương cỡ lớn và 2 bát hương nhỏ</p>

<p>Khi đặt bát hương trên ban thờ, gia chủ có thể đặt trên đế bát hương bằng sứ, đế bát hương bằng gỗ hoặc đặt trên tam cấp cho đẹp.</p>

<p>+ Lộc bình (lọ hoa): Ngoài bát hương, trên ban thờ đặt thêm lọ hoa nhỏ để cắm hoa tươi. Gia chủ có thể đặt lọ hoa theo số lượng tùy ý ở 2 bên ban thờ.</p>

<p>+ Mâm bồng (Đĩa đựng lễ vật): Một ban thờ cơ bản sẽ có 1 mâm bồng đặt chính giữa ban thờ. Mâm bồng có thể đặt hoa quả, bánh kẹo, trầu cau, tiền vàng,&hellip;</p>

<p>+ Nậm rượu: Nậm đựng rượu cúng bằng gốm sứ. Bàn thờ nhỏ gia chủ đặt 1 nậm rượu, khi cúng có thể để mở nắp nậm hoặc rót rượu ra chén.</p>

<p>+ Kỷ chén (đựng nước) và nậm rượu: Kỷ chén dùng để rót nước, rót rượu khi thờ cúng. Ban thờ có thể đặt kỷ 3 chén hoặc 5 chén ở chính giữa ban thờ hoặc ở một phía của ban thờ.</p>

<p>Đồ thờ cần có trên ban thờ truyền thống của người Việt</p>

<p>Những đồ thờ có thể đặt thêm trên ban thờ</p>

<p>Với những gia đình cầu kỳ hơn có thể đặt thêm các đồ thờ khác như:</p>

<p>+ Chóe thờ: Trong gia đình Việt ngày xưa, chóe chính là những chiếc chum lớn có nắp dùng để đựng gạo, đựng muối, đựng nước sạch. Với quan niệm trần sao thì âm vậy, nhiều gia đình đặt chóe muối, gạo, nước lên ban thờ để thờ cúng tổ tiên.</p>

<p>Các gia đình có thể đặt chóe lên ban thờ với số lượng từ 1 chóe đến 3 chóe, bên trong có thể đựng gạo, muối, nước sạch hoặc rượu, tiền. Nếu ban thờ nhỏ, gia chủ nên đặt 1 chóe sang 1 bên ban thờ. Đặt 2 chóe thì nên đặt đối xứng hai bên ban thờ. Bàn thờ kích thước lớn, gia chủ đặt 3 chóe cạnh nhau theo hình tam giác.</p>

<p>+ Bộ trà: Bộ trà cúng được đặt lên ban thờ với mục đích pha trà, dâng trà trong những ngày cúng lễ. Ban thờ có thể đặt thêm 1 bộ trà ở vị trí phía trước.</p>

<p>+ Bát sâm: Bát sâm hay còn gọi là bát trà sâm, chén trà sâm. Bát sâm có đế lót và có nắp đậy, dùng để dâng trà hoặc đựng nước trong ngày thờ cúng. Gia chủ có thể bày 1 bát sâm hoặc 2 bát sâm đối xứng nhau trên ban thờ.</p>

<p>+ Bát cúng, đũa thờ: Bát cúng, đũa thờ có thể bày trên ban thờ để tăng thêm sự đầy đủ, sung túc và tính uy nghi. Bát cúng, đũa thờ có thể bày theo bộ 6 bát 6 đũa, 10 bát 10 đũa. Bát cúng chia làm hai đặt sang 2 phía ban thờ. Đũa cúng chỉ cần đặt theo bộ đã thiết kế sẵn.</p>

<p>+ Ống hương: Ống hương đặt hương sẽ giúp ban thờ luôn gọn gàng, ngăn nắp. Ống hương có thể đặt sang một phía phan thờ, đối diện với nậm rượu.</p>

<p>+ Đèn dầu và chân nến: Trên ban thờ cần có đèn dầu, chân nến để thắp lửa, giữ lửa. Đèn dầu có thể đặt 1 đèn hoặc 2 đèn tùy vào kích thước ban thờ tại gia. Chân nến đặt cũng như vậy.</p>

<p>Đồ thờ đặt thêm giúp ban thờ gia tiên&nbsp;trở nên uy nghi đầy đủ</p>

<p>Ngoài các sản phẩm đồ thờ trên đây, nếu ban thờ tại gia kích thước rất lớn, gia chủ có thể đặt thêm các đồ thờ như:</p>

<p>+ Bộ tam sự: Chính là bộ đỉnh hạc gồm 1 lư hương và 2 con hạc. Bộ tam sự sẽ đặt chính giữa và bên trong cùng của ban thờ. Lư hương có thể đặt lên chân đế để tôn cao lên.</p>

<p>+ Bộ ngũ sự: Bao gồm bộ đỉnh hạc và 2 cây nến lớn. Bộ đỉnh hạc vẫn đặt như trên, 2 cây nến sẽ đặt phía ngoài cùng, đằng trước ban thờ và đối diện sang hai bên.</p>

<p>+ Mâm bồng: Ngoài 1 mâm bồng lớn ở chính giữa, ban thờ lớn có thể đặt thêm 2 mâm bồng nhỏ sang hai bên để đặt trầu cau, tiền vàng.</p>

<p>+ Lục bình: Gia chủ có thể đặt đôi lọ lộc bình lớn để cắm sen gỗ, cắm cành đào ngày Tết, 2 lọ lục bình nhỏ dùng để cắm hoa tươi. Đôi lộc bình sẽ đặt đối xứng hai bên ban thờ.</p>

<p><a id="Để có giá chuẩn nhất, quý khách hàng xin hãy liên hệ Hotline 0918.482.648 để nhận tư vấn trực tiếp." name="Để có giá chuẩn nhất, quý khách hàng xin hãy liên hệ Hotline 0918.482... để nhận tư vấn trực tiếp.">Để có giá chuẩn nhất, quý khách hàng xin hãy liên hệ Hotline 0918.482.648 để nhận tư vấn trực tiếp.</a></p>
', N'bo-binh-am-ruou-ty-ba-dl-08l', N'', N'', N'', N'vi', 1, 1, 0, 1, 1, N'/uploads/files/Untitled.png', CAST(N'2021-01-31T21:05:54.350' AS DateTime), CAST(N'2021-02-01T11:03:58.697' AS DateTime))
GO
INSERT [dbo].[Oderdt] ([id], [oderId], [proId], [quantity], [priceNow], [priceCount]) VALUES (N'3e3a0469-6ef4-4f44-921a-4fadfc25b875', N'934bde38-0ba1-4072-bb60-0da47edbbfc3', N'11b81dd4-00ac-48e8-a20c-ca6293862bc4', 1, 1350000, 1350000)
GO
INSERT [dbo].[Oderdt] ([id], [oderId], [proId], [quantity], [priceNow], [priceCount]) VALUES (N'954be5ad-4ffc-4deb-a003-c638a63aabb8', N'9a42f979-2716-4cdc-a093-e5d9926d713b', N'9147aa12-af10-4943-9aad-44292448184e', 1, 1350000, 1350000)
GO
INSERT [dbo].[Oders] ([id], [cusId], [total], [createDate], [status], [priceShip], [noteSiteAdmin], [noteSite], [updateDate], [updateBy]) VALUES (N'934bde38-0ba1-4072-bb60-0da47edbbfc3', N'1ac74dcc-ea73-48a6-aa13-a24e054bc4ba', 1350000, CAST(N'2021-02-02T06:36:37.820' AS DateTime), 3, 0, N' Hng da duoc giao', N'', NULL, N'1')
GO
INSERT [dbo].[Oders] ([id], [cusId], [total], [createDate], [status], [priceShip], [noteSiteAdmin], [noteSite], [updateDate], [updateBy]) VALUES (N'9a42f979-2716-4cdc-a093-e5d9926d713b', N'49b91069-8438-4696-86e7-a18787a7e26b', 1350000, CAST(N'2021-02-01T22:40:58.260' AS DateTime), 1, 0, NULL, N'', NULL, NULL)
GO
INSERT [dbo].[Partner] ([id], [pName], [pImage], [pNote], [numberOder], [pLink], [groupId], [lang]) VALUES (N'2dd54604-3eca-4d4d-b3e0-a367c221c20e', N'https://www.ceramic-japan.co.jp/en/', N'/uploads/files/20210201_101504.jpg', N'Đối tác Nhật Bản', 5, NULL, NULL, N'vi        ')
GO
INSERT [dbo].[Partner] ([id], [pName], [pImage], [pNote], [numberOder], [pLink], [groupId], [lang]) VALUES (N'3f041807-2aae-43e4-a62a-96a5a9647420', N'CÔNG TY CỔ PHẦN GỐM SỨ MINH LONG', N'/uploads/files/logogsml.png', N'Đối tác chiến lược 2', 2, NULL, NULL, N'vi        ')
GO
INSERT [dbo].[Partner] ([id], [pName], [pImage], [pNote], [numberOder], [pLink], [groupId], [lang]) VALUES (N'66b70f40-9a94-4b39-a098-fe4f147c8361', N'CÔNG TY CỔ PHẦN SỨ BÁT TRÀNG', N'/uploads/files/unnamed%20(1).jpg', N'Đối tác chiến lược 03', 3, NULL, NULL, N'vi        ')
GO
INSERT [dbo].[Partner] ([id], [pName], [pImage], [pNote], [numberOder], [pLink], [groupId], [lang]) VALUES (N'67ebded2-8dd1-4756-a33b-27ceb04ef77d', N'Gốm sứ Giang Tây, Trung Quốc', N'/uploads/files/1-2.jpg', N'Gốm sứ Giang Tây, Trung Quốc', 5, NULL, NULL, N'vi        ')
GO
INSERT [dbo].[Partner] ([id], [pName], [pImage], [pNote], [numberOder], [pLink], [groupId], [lang]) VALUES (N'a715e6b7-0f8e-4d63-904a-174156102a98', N'CÔNG TY CỔ PHẦN GỐM CHU ĐẬU', N'/uploads/files/20210201_095519%20(2).png', N'Đối tác chiến lược', 1, NULL, NULL, N'vi        ')
GO
INSERT [dbo].[Products] ([id], [pro_name], [pro_key], [proPrice], [proPrice_sale], [pro_view], [proOrder], [proAvata], [proImages5], [proImages4], [proImages3], [proImages2], [proImages1], [proFile], [brank], [desPro], [proContentTab1], [proContentTab2], [introContent], [pro_home], [pro_hot], [active], [titleSeo], [keySeo], [desSeo], [pLang], [cateId], [createDate], [updateDate], [brandId]) VALUES (N'08a0d7da-68bf-4586-9343-340f4fb350a6', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'binh-cam-hoa-trang-tri-nha-cua-hoa-tiet-tieu-dong-du-xuan-210131-2036272', 1500000, 1350000, 1, 1, N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/c12024-300x300.jpg', N'Gốm sứ Phi Long', N'', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<table width="100%">
	<tbody>
		<tr>
			<td><strong>Mã sản phẩm:</strong></td>
			<td>BCH-N235</td>
		</tr>
		<tr>
			<td><strong>Thương hiệu:</strong></td>
			<td>Gốm bát tràng</td>
		</tr>
		<tr>
			<td><strong>Thông tin:</strong></td>
			<td>Cao 43cm, ngang 20cm</td>
		</tr>
		<tr>
			<td><strong>Trọng lượng</strong></td>
			<td>5.5 kg</td>
		</tr>
	</tbody>
</table>
', 0, 1, 1, N'', N'', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'vi', N'c543570e-896f-4e7d-9373-60a4fb7cce17', CAST(N'2021-01-31T18:52:22.877' AS DateTime), CAST(N'2021-01-31T18:53:37.410' AS DateTime), N'8858ea2b-a01f-43ac-859d-7e838aca5c00')
GO
INSERT [dbo].[Products] ([id], [pro_name], [pro_key], [proPrice], [proPrice_sale], [pro_view], [proOrder], [proAvata], [proImages5], [proImages4], [proImages3], [proImages2], [proImages1], [proFile], [brank], [desPro], [proContentTab1], [proContentTab2], [introContent], [pro_home], [pro_hot], [active], [titleSeo], [keySeo], [desSeo], [pLang], [cateId], [createDate], [updateDate], [brandId]) VALUES (N'0daeb37c-5594-4f59-ae54-9bdfd390b1e3', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'binh-cam-hoa-trang-tri-nha-cua-hoa-tiet-tieu-dong-du-xuan-210131-2040105', 1500000, 1350000, 1, 1, N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/c12024-300x300.jpg', N'Gốm sứ Phi Long', N'', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<table width="100%">
	<tbody>
		<tr>
			<td><strong>Mã sản phẩm:</strong></td>
			<td>BCH-N235</td>
		</tr>
		<tr>
			<td><strong>Thương hiệu:</strong></td>
			<td>Gốm bát tràng</td>
		</tr>
		<tr>
			<td><strong>Thông tin:</strong></td>
			<td>Cao 43cm, ngang 20cm</td>
		</tr>
		<tr>
			<td><strong>Trọng lượng</strong></td>
			<td>5.5 kg</td>
		</tr>
	</tbody>
</table>
', 0, 1, 1, N'', N'', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'vi', N'769dacd5-6cf3-4e9f-8c4f-f2a8b64c3b8c', CAST(N'2021-01-31T18:52:22.877' AS DateTime), CAST(N'2021-01-31T18:53:37.410' AS DateTime), N'8858ea2b-a01f-43ac-859d-7e838aca5c00')
GO
INSERT [dbo].[Products] ([id], [pro_name], [pro_key], [proPrice], [proPrice_sale], [pro_view], [proOrder], [proAvata], [proImages5], [proImages4], [proImages3], [proImages2], [proImages1], [proFile], [brank], [desPro], [proContentTab1], [proContentTab2], [introContent], [pro_home], [pro_hot], [active], [titleSeo], [keySeo], [desSeo], [pLang], [cateId], [createDate], [updateDate], [brandId]) VALUES (N'11b81dd4-00ac-48e8-a20c-ca6293862bc4', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'binh-cam-hoa-trang-tri-nha-cua-hoa-tiet-tieu-dong-du-xuan-210131-2040104', 1500000, 1350000, 1, 1, N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/c12024-300x300.jpg', N'Gốm sứ Phi Long', N'', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<table width="100%">
	<tbody>
		<tr>
			<td><strong>Mã sản phẩm:</strong></td>
			<td>BCH-N235</td>
		</tr>
		<tr>
			<td><strong>Thương hiệu:</strong></td>
			<td>Gốm bát tràng</td>
		</tr>
		<tr>
			<td><strong>Thông tin:</strong></td>
			<td>Cao 43cm, ngang 20cm</td>
		</tr>
		<tr>
			<td><strong>Trọng lượng</strong></td>
			<td>5.5 kg</td>
		</tr>
	</tbody>
</table>
', 0, 1, 1, N'', N'', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'vi', N'769dacd5-6cf3-4e9f-8c4f-f2a8b64c3b8c', CAST(N'2021-01-31T18:52:22.877' AS DateTime), CAST(N'2021-01-31T18:53:37.410' AS DateTime), N'8858ea2b-a01f-43ac-859d-7e838aca5c00')
GO
INSERT [dbo].[Products] ([id], [pro_name], [pro_key], [proPrice], [proPrice_sale], [pro_view], [proOrder], [proAvata], [proImages5], [proImages4], [proImages3], [proImages2], [proImages1], [proFile], [brank], [desPro], [proContentTab1], [proContentTab2], [introContent], [pro_home], [pro_hot], [active], [titleSeo], [keySeo], [desSeo], [pLang], [cateId], [createDate], [updateDate], [brandId]) VALUES (N'12bc7648-afca-4437-8928-dcf7965d9535', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'binh-cam-hoa-trang-tri-nha-cua-hoa-tiet-tieu-dong-du-xuan-210131-2039385', 1500000, 1350000, 1, 1, N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/c12024-300x300.jpg', N'Gốm sứ Phi Long', N'', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<table width="100%">
	<tbody>
		<tr>
			<td><strong>Mã sản phẩm:</strong></td>
			<td>BCH-N235</td>
		</tr>
		<tr>
			<td><strong>Thương hiệu:</strong></td>
			<td>Gốm bát tràng</td>
		</tr>
		<tr>
			<td><strong>Thông tin:</strong></td>
			<td>Cao 43cm, ngang 20cm</td>
		</tr>
		<tr>
			<td><strong>Trọng lượng</strong></td>
			<td>5.5 kg</td>
		</tr>
	</tbody>
</table>
', 0, 1, 1, N'', N'', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'vi', N'95c980b5-2ad6-45ee-8d14-0d7f6827100e', CAST(N'2021-01-31T18:52:22.877' AS DateTime), CAST(N'2021-01-31T18:53:37.410' AS DateTime), N'8858ea2b-a01f-43ac-859d-7e838aca5c00')
GO
INSERT [dbo].[Products] ([id], [pro_name], [pro_key], [proPrice], [proPrice_sale], [pro_view], [proOrder], [proAvata], [proImages5], [proImages4], [proImages3], [proImages2], [proImages1], [proFile], [brank], [desPro], [proContentTab1], [proContentTab2], [introContent], [pro_home], [pro_hot], [active], [titleSeo], [keySeo], [desSeo], [pLang], [cateId], [createDate], [updateDate], [brandId]) VALUES (N'195e2fba-1d26-4b6d-87e0-a4260c47258f', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'binh-cam-hoa-trang-tri-nha-cua-hoa-tiet-tieu-dong-du-xuan-210131-2036270', 1500000, 1350000, 1, 1, N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/c12024-300x300.jpg', N'Gốm sứ Phi Long', N'', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<table width="100%">
	<tbody>
		<tr>
			<td><strong>Mã sản phẩm:</strong></td>
			<td>BCH-N235</td>
		</tr>
		<tr>
			<td><strong>Thương hiệu:</strong></td>
			<td>Gốm bát tràng</td>
		</tr>
		<tr>
			<td><strong>Thông tin:</strong></td>
			<td>Cao 43cm, ngang 20cm</td>
		</tr>
		<tr>
			<td><strong>Trọng lượng</strong></td>
			<td>5.5 kg</td>
		</tr>
	</tbody>
</table>
', 0, 1, 1, N'', N'', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'vi', N'c543570e-896f-4e7d-9373-60a4fb7cce17', CAST(N'2021-01-31T18:52:22.877' AS DateTime), CAST(N'2021-01-31T18:53:37.410' AS DateTime), N'8858ea2b-a01f-43ac-859d-7e838aca5c00')
GO
INSERT [dbo].[Products] ([id], [pro_name], [pro_key], [proPrice], [proPrice_sale], [pro_view], [proOrder], [proAvata], [proImages5], [proImages4], [proImages3], [proImages2], [proImages1], [proFile], [brank], [desPro], [proContentTab1], [proContentTab2], [introContent], [pro_home], [pro_hot], [active], [titleSeo], [keySeo], [desSeo], [pLang], [cateId], [createDate], [updateDate], [brandId]) VALUES (N'4f3ec319-e75e-487a-ac46-c0093de5da00', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'binh-cam-hoa-trang-tri-nha-cua-hoa-tiet-tieu-dong-du-xuan-210131-2036275', 1500000, 1350000, 1, 1, N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/c12024-300x300.jpg', N'Gốm sứ Phi Long', N'', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<table width="100%">
	<tbody>
		<tr>
			<td><strong>Mã sản phẩm:</strong></td>
			<td>BCH-N235</td>
		</tr>
		<tr>
			<td><strong>Thương hiệu:</strong></td>
			<td>Gốm bát tràng</td>
		</tr>
		<tr>
			<td><strong>Thông tin:</strong></td>
			<td>Cao 43cm, ngang 20cm</td>
		</tr>
		<tr>
			<td><strong>Trọng lượng</strong></td>
			<td>5.5 kg</td>
		</tr>
	</tbody>
</table>
', 0, 1, 1, N'', N'', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'vi', N'c543570e-896f-4e7d-9373-60a4fb7cce17', CAST(N'2021-01-31T18:52:22.877' AS DateTime), CAST(N'2021-01-31T18:53:37.410' AS DateTime), N'8858ea2b-a01f-43ac-859d-7e838aca5c00')
GO
INSERT [dbo].[Products] ([id], [pro_name], [pro_key], [proPrice], [proPrice_sale], [pro_view], [proOrder], [proAvata], [proImages5], [proImages4], [proImages3], [proImages2], [proImages1], [proFile], [brank], [desPro], [proContentTab1], [proContentTab2], [introContent], [pro_home], [pro_hot], [active], [titleSeo], [keySeo], [desSeo], [pLang], [cateId], [createDate], [updateDate], [brandId]) VALUES (N'5512cbfd-9de4-4a00-a818-0d1fd131e039', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'binh-cam-hoa-trang-tri-nha-cua-hoa-tiet-tieu-dong-du-xuan-210131-2040103', 1500000, 1350000, 1, 1, N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/c12024-300x300.jpg', N'Gốm sứ Phi Long', N'', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<table width="100%">
	<tbody>
		<tr>
			<td><strong>Mã sản phẩm:</strong></td>
			<td>BCH-N235</td>
		</tr>
		<tr>
			<td><strong>Thương hiệu:</strong></td>
			<td>Gốm bát tràng</td>
		</tr>
		<tr>
			<td><strong>Thông tin:</strong></td>
			<td>Cao 43cm, ngang 20cm</td>
		</tr>
		<tr>
			<td><strong>Trọng lượng</strong></td>
			<td>5.5 kg</td>
		</tr>
	</tbody>
</table>
', 0, 1, 1, N'', N'', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'vi', N'769dacd5-6cf3-4e9f-8c4f-f2a8b64c3b8c', CAST(N'2021-01-31T18:52:22.877' AS DateTime), CAST(N'2021-01-31T18:53:37.410' AS DateTime), N'8858ea2b-a01f-43ac-859d-7e838aca5c00')
GO
INSERT [dbo].[Products] ([id], [pro_name], [pro_key], [proPrice], [proPrice_sale], [pro_view], [proOrder], [proAvata], [proImages5], [proImages4], [proImages3], [proImages2], [proImages1], [proFile], [brank], [desPro], [proContentTab1], [proContentTab2], [introContent], [pro_home], [pro_hot], [active], [titleSeo], [keySeo], [desSeo], [pLang], [cateId], [createDate], [updateDate], [brandId]) VALUES (N'8636387c-9445-4755-a3c4-cfcf21eb39e4', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'binh-cam-hoa-trang-tri-nha-cua-hoa-tiet-tieu-dong-du-xuan-210131-2036273', 1500000, 1350000, 1, 1, N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/c12024-300x300.jpg', N'Gốm sứ Phi Long', N'', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<table width="100%">
	<tbody>
		<tr>
			<td><strong>Mã sản phẩm:</strong></td>
			<td>BCH-N235</td>
		</tr>
		<tr>
			<td><strong>Thương hiệu:</strong></td>
			<td>Gốm bát tràng</td>
		</tr>
		<tr>
			<td><strong>Thông tin:</strong></td>
			<td>Cao 43cm, ngang 20cm</td>
		</tr>
		<tr>
			<td><strong>Trọng lượng</strong></td>
			<td>5.5 kg</td>
		</tr>
	</tbody>
</table>
', 0, 1, 1, N'', N'', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'vi', N'c543570e-896f-4e7d-9373-60a4fb7cce17', CAST(N'2021-01-31T18:52:22.877' AS DateTime), CAST(N'2021-01-31T18:53:37.410' AS DateTime), N'8858ea2b-a01f-43ac-859d-7e838aca5c00')
GO
INSERT [dbo].[Products] ([id], [pro_name], [pro_key], [proPrice], [proPrice_sale], [pro_view], [proOrder], [proAvata], [proImages5], [proImages4], [proImages3], [proImages2], [proImages1], [proFile], [brank], [desPro], [proContentTab1], [proContentTab2], [introContent], [pro_home], [pro_hot], [active], [titleSeo], [keySeo], [desSeo], [pLang], [cateId], [createDate], [updateDate], [brandId]) VALUES (N'9147aa12-af10-4943-9aad-44292448184e', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'binh-cam-hoa-trang-tri-nha-cua-hoa-tiet-tieu-dong-du-xuan', 1500000, 1350000, 1, 1, N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', NULL, N'Gốm sứ Phi Long', N'', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<table width="100%">
	<tbody>
		<tr>
			<td><strong>Mã sản phẩm:</strong></td>
			<td>BCH-N235</td>
		</tr>
		<tr>
			<td><strong>Thương hiệu:</strong></td>
			<td>Gốm bát tràng</td>
		</tr>
		<tr>
			<td><strong>Thông tin:</strong></td>
			<td>Cao 43cm, ngang 20cm</td>
		</tr>
		<tr>
			<td><strong>Trọng lượng</strong></td>
			<td>5.5 kg</td>
		</tr>
	</tbody>
</table>
', 0, 1, 1, N'', N'', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'vi', N'c543570e-896f-4e7d-9373-60a4fb7cce17', CAST(N'2021-01-31T18:52:22.877' AS DateTime), CAST(N'2021-01-31T18:53:37.410' AS DateTime), N'8858ea2b-a01f-43ac-859d-7e838aca5c00')
GO
INSERT [dbo].[Products] ([id], [pro_name], [pro_key], [proPrice], [proPrice_sale], [pro_view], [proOrder], [proAvata], [proImages5], [proImages4], [proImages3], [proImages2], [proImages1], [proFile], [brank], [desPro], [proContentTab1], [proContentTab2], [introContent], [pro_home], [pro_hot], [active], [titleSeo], [keySeo], [desSeo], [pLang], [cateId], [createDate], [updateDate], [brandId]) VALUES (N'91a5868a-98c1-4b32-89f7-f6ad572302e6', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'binh-cam-hoa-trang-tri-nha-cua-hoa-tiet-tieu-dong-du-xuan-210131-2036271', 1500000, 1350000, 1, 1, N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/c12024-300x300.jpg', N'Gốm sứ Phi Long', N'', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<table width="100%">
	<tbody>
		<tr>
			<td><strong>Mã sản phẩm:</strong></td>
			<td>BCH-N235</td>
		</tr>
		<tr>
			<td><strong>Thương hiệu:</strong></td>
			<td>Gốm bát tràng</td>
		</tr>
		<tr>
			<td><strong>Thông tin:</strong></td>
			<td>Cao 43cm, ngang 20cm</td>
		</tr>
		<tr>
			<td><strong>Trọng lượng</strong></td>
			<td>5.5 kg</td>
		</tr>
	</tbody>
</table>
', 0, 1, 1, N'', N'', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'vi', N'c543570e-896f-4e7d-9373-60a4fb7cce17', CAST(N'2021-01-31T18:52:22.877' AS DateTime), CAST(N'2021-01-31T18:53:37.410' AS DateTime), N'8858ea2b-a01f-43ac-859d-7e838aca5c00')
GO
INSERT [dbo].[Products] ([id], [pro_name], [pro_key], [proPrice], [proPrice_sale], [pro_view], [proOrder], [proAvata], [proImages5], [proImages4], [proImages3], [proImages2], [proImages1], [proFile], [brank], [desPro], [proContentTab1], [proContentTab2], [introContent], [pro_home], [pro_hot], [active], [titleSeo], [keySeo], [desSeo], [pLang], [cateId], [createDate], [updateDate], [brandId]) VALUES (N'b483106e-acd4-4ae5-be8e-0742cb21a5d8', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'binh-cam-hoa-trang-tri-nha-cua-hoa-tiet-tieu-dong-du-xuan-210131-2039382', 1500000, 1350000, 1, 1, N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/c12024-300x300.jpg', N'Gốm sứ Phi Long', N'', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<table width="100%">
	<tbody>
		<tr>
			<td><strong>Mã sản phẩm:</strong></td>
			<td>BCH-N235</td>
		</tr>
		<tr>
			<td><strong>Thương hiệu:</strong></td>
			<td>Gốm bát tràng</td>
		</tr>
		<tr>
			<td><strong>Thông tin:</strong></td>
			<td>Cao 43cm, ngang 20cm</td>
		</tr>
		<tr>
			<td><strong>Trọng lượng</strong></td>
			<td>5.5 kg</td>
		</tr>
	</tbody>
</table>
', 0, 1, 1, N'', N'', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'vi', N'95c980b5-2ad6-45ee-8d14-0d7f6827100e', CAST(N'2021-01-31T18:52:22.877' AS DateTime), CAST(N'2021-01-31T18:53:37.410' AS DateTime), N'8858ea2b-a01f-43ac-859d-7e838aca5c00')
GO
INSERT [dbo].[Products] ([id], [pro_name], [pro_key], [proPrice], [proPrice_sale], [pro_view], [proOrder], [proAvata], [proImages5], [proImages4], [proImages3], [proImages2], [proImages1], [proFile], [brank], [desPro], [proContentTab1], [proContentTab2], [introContent], [pro_home], [pro_hot], [active], [titleSeo], [keySeo], [desSeo], [pLang], [cateId], [createDate], [updateDate], [brandId]) VALUES (N'b69a0647-da8b-4574-acfe-b8781e02dde4', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'binh-cam-hoa-trang-tri-nha-cua-hoa-tiet-tieu-dong-du-xuan-210131-2039381', 1500000, 1350000, 1, 1, N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/c12024-300x300.jpg', N'Gốm sứ Phi Long', N'', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<table width="100%">
	<tbody>
		<tr>
			<td><strong>Mã sản phẩm:</strong></td>
			<td>BCH-N235</td>
		</tr>
		<tr>
			<td><strong>Thương hiệu:</strong></td>
			<td>Gốm bát tràng</td>
		</tr>
		<tr>
			<td><strong>Thông tin:</strong></td>
			<td>Cao 43cm, ngang 20cm</td>
		</tr>
		<tr>
			<td><strong>Trọng lượng</strong></td>
			<td>5.5 kg</td>
		</tr>
	</tbody>
</table>
', 0, 1, 1, N'', N'', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'vi', N'95c980b5-2ad6-45ee-8d14-0d7f6827100e', CAST(N'2021-01-31T18:52:22.877' AS DateTime), CAST(N'2021-01-31T18:53:37.410' AS DateTime), N'8858ea2b-a01f-43ac-859d-7e838aca5c00')
GO
INSERT [dbo].[Products] ([id], [pro_name], [pro_key], [proPrice], [proPrice_sale], [pro_view], [proOrder], [proAvata], [proImages5], [proImages4], [proImages3], [proImages2], [proImages1], [proFile], [brank], [desPro], [proContentTab1], [proContentTab2], [introContent], [pro_home], [pro_hot], [active], [titleSeo], [keySeo], [desSeo], [pLang], [cateId], [createDate], [updateDate], [brandId]) VALUES (N'cdb80737-900e-4ae4-8ef9-9e3c657b547e', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'binh-cam-hoa-trang-tri-nha-cua-hoa-tiet-tieu-dong-du-xuan-210131-2040102', 1500000, 1350000, 1, 1, N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/c12024-300x300.jpg', N'Gốm sứ Phi Long', N'', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<table width="100%">
	<tbody>
		<tr>
			<td><strong>Mã sản phẩm:</strong></td>
			<td>BCH-N235</td>
		</tr>
		<tr>
			<td><strong>Thương hiệu:</strong></td>
			<td>Gốm bát tràng</td>
		</tr>
		<tr>
			<td><strong>Thông tin:</strong></td>
			<td>Cao 43cm, ngang 20cm</td>
		</tr>
		<tr>
			<td><strong>Trọng lượng</strong></td>
			<td>5.5 kg</td>
		</tr>
	</tbody>
</table>
', 0, 1, 1, N'', N'', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'vi', N'769dacd5-6cf3-4e9f-8c4f-f2a8b64c3b8c', CAST(N'2021-01-31T18:52:22.877' AS DateTime), CAST(N'2021-01-31T18:53:37.410' AS DateTime), N'8858ea2b-a01f-43ac-859d-7e838aca5c00')
GO
INSERT [dbo].[Products] ([id], [pro_name], [pro_key], [proPrice], [proPrice_sale], [pro_view], [proOrder], [proAvata], [proImages5], [proImages4], [proImages3], [proImages2], [proImages1], [proFile], [brank], [desPro], [proContentTab1], [proContentTab2], [introContent], [pro_home], [pro_hot], [active], [titleSeo], [keySeo], [desSeo], [pLang], [cateId], [createDate], [updateDate], [brandId]) VALUES (N'deb30668-57f0-40ef-9a2e-3adffcaaf237', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'binh-cam-hoa-trang-tri-nha-cua-hoa-tiet-tieu-dong-du-xuan-210131-2040100', 1500000, 1350000, 1, 1, N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/c12024-300x300.jpg', N'Gốm sứ Phi Long', N'', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<table width="100%">
	<tbody>
		<tr>
			<td><strong>Mã sản phẩm:</strong></td>
			<td>BCH-N235</td>
		</tr>
		<tr>
			<td><strong>Thương hiệu:</strong></td>
			<td>Gốm bát tràng</td>
		</tr>
		<tr>
			<td><strong>Thông tin:</strong></td>
			<td>Cao 43cm, ngang 20cm</td>
		</tr>
		<tr>
			<td><strong>Trọng lượng</strong></td>
			<td>5.5 kg</td>
		</tr>
	</tbody>
</table>
', 0, 1, 1, N'', N'', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'vi', N'769dacd5-6cf3-4e9f-8c4f-f2a8b64c3b8c', CAST(N'2021-01-31T18:52:22.877' AS DateTime), CAST(N'2021-01-31T18:53:37.410' AS DateTime), N'8858ea2b-a01f-43ac-859d-7e838aca5c00')
GO
INSERT [dbo].[Products] ([id], [pro_name], [pro_key], [proPrice], [proPrice_sale], [pro_view], [proOrder], [proAvata], [proImages5], [proImages4], [proImages3], [proImages2], [proImages1], [proFile], [brank], [desPro], [proContentTab1], [proContentTab2], [introContent], [pro_home], [pro_hot], [active], [titleSeo], [keySeo], [desSeo], [pLang], [cateId], [createDate], [updateDate], [brandId]) VALUES (N'df03b912-d416-4afa-ba77-824970ac5793', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'binh-cam-hoa-trang-tri-nha-cua-hoa-tiet-tieu-dong-du-xuan-210131-2039380', 1500000, 1350000, 1, 1, N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/c12024-300x300.jpg', N'Gốm sứ Phi Long', N'', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<table width="100%">
	<tbody>
		<tr>
			<td><strong>Mã sản phẩm:</strong></td>
			<td>BCH-N235</td>
		</tr>
		<tr>
			<td><strong>Thương hiệu:</strong></td>
			<td>Gốm bát tràng</td>
		</tr>
		<tr>
			<td><strong>Thông tin:</strong></td>
			<td>Cao 43cm, ngang 20cm</td>
		</tr>
		<tr>
			<td><strong>Trọng lượng</strong></td>
			<td>5.5 kg</td>
		</tr>
	</tbody>
</table>
', 0, 1, 1, N'', N'', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'vi', N'95c980b5-2ad6-45ee-8d14-0d7f6827100e', CAST(N'2021-01-31T18:52:22.877' AS DateTime), CAST(N'2021-01-31T18:53:37.410' AS DateTime), N'8858ea2b-a01f-43ac-859d-7e838aca5c00')
GO
INSERT [dbo].[Products] ([id], [pro_name], [pro_key], [proPrice], [proPrice_sale], [pro_view], [proOrder], [proAvata], [proImages5], [proImages4], [proImages3], [proImages2], [proImages1], [proFile], [brank], [desPro], [proContentTab1], [proContentTab2], [introContent], [pro_home], [pro_hot], [active], [titleSeo], [keySeo], [desSeo], [pLang], [cateId], [createDate], [updateDate], [brandId]) VALUES (N'dff021b4-07fb-40f5-a140-d6e68de2c99e', N'BÌNH CẮM HOA ', N'binh-cam-hoa-', 1500000, 1350000, 1, 1, N'/uploads/files/1-2.jpg', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', NULL, N'Gốm sứ Phi Long', N'', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<table width="100%">
	<tbody>
		<tr>
			<td><strong>Mã sản phẩm:</strong></td>
			<td>BCH-N235</td>
		</tr>
		<tr>
			<td><strong>Thương hiệu:</strong></td>
			<td>Gốm bát tràng</td>
		</tr>
		<tr>
			<td><strong>Thông tin:</strong></td>
			<td>Cao 43cm, ngang 20cm</td>
		</tr>
		<tr>
			<td><strong>Trọng lượng</strong></td>
			<td>5.5 kg</td>
		</tr>
	</tbody>
</table>
', 0, 1, 1, N'', N'', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'vi', N'95c980b5-2ad6-45ee-8d14-0d7f6827100e', CAST(N'2021-01-31T18:52:22.877' AS DateTime), CAST(N'2021-02-01T21:16:26.397' AS DateTime), N'8858ea2b-a01f-43ac-859d-7e838aca5c00')
GO
INSERT [dbo].[Products] ([id], [pro_name], [pro_key], [proPrice], [proPrice_sale], [pro_view], [proOrder], [proAvata], [proImages5], [proImages4], [proImages3], [proImages2], [proImages1], [proFile], [brank], [desPro], [proContentTab1], [proContentTab2], [introContent], [pro_home], [pro_hot], [active], [titleSeo], [keySeo], [desSeo], [pLang], [cateId], [createDate], [updateDate], [brandId]) VALUES (N'e018a8c8-f5b0-40f2-aced-a12e839f605e', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'binh-cam-hoa-trang-tri-nha-cua-hoa-tiet-tieu-dong-du-xuan-210131-2040101', 1500000, 1350000, 1, 1, N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/c12024-300x300.jpg', N'Gốm sứ Phi Long', N'', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<table width="100%">
	<tbody>
		<tr>
			<td><strong>Mã sản phẩm:</strong></td>
			<td>BCH-N235</td>
		</tr>
		<tr>
			<td><strong>Thương hiệu:</strong></td>
			<td>Gốm bát tràng</td>
		</tr>
		<tr>
			<td><strong>Thông tin:</strong></td>
			<td>Cao 43cm, ngang 20cm</td>
		</tr>
		<tr>
			<td><strong>Trọng lượng</strong></td>
			<td>5.5 kg</td>
		</tr>
	</tbody>
</table>
', 0, 1, 1, N'', N'', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'vi', N'769dacd5-6cf3-4e9f-8c4f-f2a8b64c3b8c', CAST(N'2021-01-31T18:52:22.877' AS DateTime), CAST(N'2021-01-31T18:53:37.410' AS DateTime), N'8858ea2b-a01f-43ac-859d-7e838aca5c00')
GO
INSERT [dbo].[Products] ([id], [pro_name], [pro_key], [proPrice], [proPrice_sale], [pro_view], [proOrder], [proAvata], [proImages5], [proImages4], [proImages3], [proImages2], [proImages1], [proFile], [brank], [desPro], [proContentTab1], [proContentTab2], [introContent], [pro_home], [pro_hot], [active], [titleSeo], [keySeo], [desSeo], [pLang], [cateId], [createDate], [updateDate], [brandId]) VALUES (N'e0dbf40d-6afe-4f19-b862-556830ffd979', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'binh-cam-hoa-trang-tri-nha-cua-hoa-tiet-tieu-dong-du-xuan-210131-2039383', 1500000, 1350000, 1, 1, N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/c12024-300x300.jpg', N'Gốm sứ Phi Long', N'', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<table width="100%">
	<tbody>
		<tr>
			<td><strong>Mã sản phẩm:</strong></td>
			<td>BCH-N235</td>
		</tr>
		<tr>
			<td><strong>Thương hiệu:</strong></td>
			<td>Gốm bát tràng</td>
		</tr>
		<tr>
			<td><strong>Thông tin:</strong></td>
			<td>Cao 43cm, ngang 20cm</td>
		</tr>
		<tr>
			<td><strong>Trọng lượng</strong></td>
			<td>5.5 kg</td>
		</tr>
	</tbody>
</table>
', 0, 1, 1, N'', N'', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'vi', N'95c980b5-2ad6-45ee-8d14-0d7f6827100e', CAST(N'2021-01-31T18:52:22.877' AS DateTime), CAST(N'2021-01-31T18:53:37.410' AS DateTime), N'8858ea2b-a01f-43ac-859d-7e838aca5c00')
GO
INSERT [dbo].[Products] ([id], [pro_name], [pro_key], [proPrice], [proPrice_sale], [pro_view], [proOrder], [proAvata], [proImages5], [proImages4], [proImages3], [proImages2], [proImages1], [proFile], [brank], [desPro], [proContentTab1], [proContentTab2], [introContent], [pro_home], [pro_hot], [active], [titleSeo], [keySeo], [desSeo], [pLang], [cateId], [createDate], [updateDate], [brandId]) VALUES (N'f8e27227-1e71-45e7-a32c-8a2eeba0fe0e', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'binh-cam-hoa-trang-tri-nha-cua-hoa-tiet-tieu-dong-du-xuan-210131-2039384', 1500000, 1350000, 1, 1, N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/c12024-300x300.jpg', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/15470191372129096545.png', N'/uploads/products/c12024-300x300.jpg', N'Gốm sứ Phi Long', N'', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<p>GỐM SỨ TRANG TRÍ ĐẢM BẢO: *** Công khai giá trên từng sp... *** Bao vỡ hỏng, 1 đền 1 cho khách nếu phát hiện sp lỗi *** Khuyến khích khách hàng kiểm tra sp trước khi thanh toán... Shop nhận ship COD trên toàn quốc...Phí ship dao động từ 20 - 50k tùy từng tỉnh. Để đảm bảo quyền lợi cho các khách mua hàng online...Khách hàng được quyền kiểm tra hàng thoải mái trước khi thanh toán...Vì vậy các khách có thể hoàn toàn yên tâm khi mua sp tại shop, không sợ bùng hàng, không sợ hàng không đúng như hình ah... ---------------------------------------------------------------------------------------------------- Đặc biệt, tất cả các khách hàng mua hàng trực tiếp tại cửa hàng theo địa chỉ bên dưới đều được giảm 5% trên tổng giá trị sp đã mua..</p>
', N'<table width="100%">
	<tbody>
		<tr>
			<td><strong>Mã sản phẩm:</strong></td>
			<td>BCH-N235</td>
		</tr>
		<tr>
			<td><strong>Thương hiệu:</strong></td>
			<td>Gốm bát tràng</td>
		</tr>
		<tr>
			<td><strong>Thông tin:</strong></td>
			<td>Cao 43cm, ngang 20cm</td>
		</tr>
		<tr>
			<td><strong>Trọng lượng</strong></td>
			<td>5.5 kg</td>
		</tr>
	</tbody>
</table>
', 0, 1, 1, N'', N'', N'BÌNH CẮM HOA - TRANG TRÍ NHÀ CỬA HỌA TIẾT TIỂU ĐỒNG DU XUÂN', N'vi', N'95c980b5-2ad6-45ee-8d14-0d7f6827100e', CAST(N'2021-01-31T18:52:22.877' AS DateTime), CAST(N'2021-01-31T18:53:37.410' AS DateTime), N'8858ea2b-a01f-43ac-859d-7e838aca5c00')
GO
INSERT [dbo].[Slide] ([id], [name], [images], [contents], [active], [slang], [sLink], [numberOder]) VALUES (N'b691650a-5c6e-4a1e-a024-17b62faeb278', N'Gốm PhiLong', N'/uploads/advi/001.jpg', NULL, 1, N'vi', N'/', 1)
GO
INSERT [dbo].[Support] ([id], [sType], [nick], [phone], [email], [addresss], [fullName], [numberOder]) VALUES (N'ad386ce9-1646-4684-aab0-a1c0d2fa1ef9', 3, N'0983190412', N'0983190412', N'huyentrang1610@gmail.com', N'Hà Nội', N'Ms. Huyền', 2)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Customer__AB6E6164B6BEB875]    Script Date: 2/2/2021 8:15:12 AM ******/
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [UQ__Customer__AB6E6164B6BEB875] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF__Products__pro_vi__31EC6D26]  DEFAULT ((0)) FOR [pro_view]
GO
ALTER TABLE [dbo].[News]  WITH CHECK ADD  CONSTRAINT [FK__News__groupid__1BFD2C07] FOREIGN KEY([groupId])
REFERENCES [dbo].[Categorys] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[News] CHECK CONSTRAINT [FK__News__groupid__1BFD2C07]
GO
ALTER TABLE [dbo].[Oderdt]  WITH CHECK ADD  CONSTRAINT [FK__Oderdt__oderid__34C8D9D1] FOREIGN KEY([oderId])
REFERENCES [dbo].[Oders] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Oderdt] CHECK CONSTRAINT [FK__Oderdt__oderid__34C8D9D1]
GO
USE [master]
GO
ALTER DATABASE [gom51287_gomsu] SET  READ_WRITE 
GO
