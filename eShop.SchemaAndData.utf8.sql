USE [master]
GO
/****** Object:  Database [eShop]    Script Date: 2020-09-05 12:07:44 PM ******/
CREATE DATABASE [eShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'eShop', FILENAME = N'E:\ProgramFiles\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\eShop.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'eShop_log', FILENAME = N'E:\ProgramFiles\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\eShop_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [eShop] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [eShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [eShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [eShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [eShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [eShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [eShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [eShop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [eShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [eShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [eShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [eShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [eShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [eShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [eShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [eShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [eShop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [eShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [eShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [eShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [eShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [eShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [eShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [eShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [eShop] SET RECOVERY FULL 
GO
ALTER DATABASE [eShop] SET  MULTI_USER 
GO
ALTER DATABASE [eShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [eShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [eShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [eShop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [eShop] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'eShop', N'ON'
GO
ALTER DATABASE [eShop] SET QUERY_STORE = OFF
GO
USE [eShop]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [eShop]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 2020-09-05 12:07:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[DatePlaced] [datetime] NULL,
	[DateProcessing] [datetime] NULL,
	[DateProcessed] [datetime] NULL,
	[CustomerName] [nvarchar](150) NULL,
	[CustomerAddress] [nvarchar](150) NULL,
	[CustomerCity] [nvarchar](50) NULL,
	[CustomerStateProvince] [nvarchar](50) NULL,
	[CustomerCountry] [nvarchar](50) NULL,
	[AdminUser] [nvarchar](150) NULL,
	[UniqueId] [nvarchar](100) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderLineItem]    Script Date: 2020-09-05 12:07:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderLineItem](
	[LineItemId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[OrderId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [float] NOT NULL,
 CONSTRAINT [PK_OrderLineItem] PRIMARY KEY CLUSTERED 
(
	[LineItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 2020-09-05 12:07:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] NOT NULL,
	[Brand] [nvarchar](150) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Price] [float] NOT NULL,
	[ImageLink] [nvarchar](500) NULL,
	[Description] [nvarchar](1500) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Product] ([ProductId], [Brand], [Name], [Price], [ImageLink], [Description]) VALUES (286, N'maybelline', N'Maybelline The Nudes Eyeshadow Palette in The Blushed Nudes', 17.99, N'https://d3t32hsnjxo7q6.cloudfront.net/i/49d98e112e77d2a9a0c8fad28df89a1e_ra,w158,h184_pa,w158,h184.png', N'Create looks from day to night and deep to light with Maybelline''s The Blushed Nudes Eyeshadow Palette.Features:13 looks in one eyeshadow paletteExtraordinary colour from ultra-blendable pigmentsLong wear with sensual finish that lasts up to 12 hours')
INSERT [dbo].[Product] ([ProductId], [Brand], [Name], [Price], [ImageLink], [Description]) VALUES (291, N'maybelline', N'Maybelline Eye Studio Color Tattoo 24HR Cream Gel Shadow Leather', 8.99, N'https://d3t32hsnjxo7q6.cloudfront.net/i/cf21d194ab14ee3c527d02682c358a7a_ra,w158,h184_pa,w158,h184.png', N'                          So rich. So creamy. Only Maybelline''s cream gel eye shadow formula gets the look of couture leather so right!            Dare to wear Maybelline Color Tattoo leather 24HR long wear eye shadow.Features: Ink technology creates 24HR wear shadow for our most intense, long-lasting color saturationCream eye shadow formula glides on without creasing and keeps color vibrantOphthalmologist-tested.  Safe for sensitive eyes and contact lens wearers   Shade Range:                                          ')
INSERT [dbo].[Product] ([ProductId], [Brand], [Name], [Price], [ImageLink], [Description]) VALUES (295, N'maybelline', N'Maybelline The Nudes Eye Shadow Palette', 17.99, N'https://d3t32hsnjxo7q6.cloudfront.net/i/201350fd3e173307ade44520dc87d8fb_ra,w158,h184_pa,w158,h184.png', N'Maybelline The Nudes Eye Shadow Palette let''s you create looks from day to night. Deep to light. So try it out and create your ideal eye look today!Features:             13 looks in one Expert Wear eyeshadow palette Extraordinary Colour, from soft highlights, to medium bronze, to rich black From Maybelline''s ultra-blendable pigments Long Wear            ')
INSERT [dbo].[Product] ([ProductId], [Brand], [Name], [Price], [ImageLink], [Description]) VALUES (307, N'maybelline', N'Maybelline Eyestudio Color Tattoo Concentrated Crayon', 10.99, N'https://d3t32hsnjxo7q6.cloudfront.net/i/3f9f894b56e0616e44c5ee01dea45217_ra,w158,h184_pa,w158,h184.png', N'Maybelline Eyestudio Color Tattoo Concentrated Crayons give you high-intensity color that looks vibrant all-day long.Features:Smooth, soft creamy finishPlayful intense colorsAll day tattoo tenacity. Playful color intensity. In an easy glide on crayon.')
INSERT [dbo].[Product] ([ProductId], [Brand], [Name], [Price], [ImageLink], [Description]) VALUES (309, N'maybelline', N'Maybelline Expert Wear Eye Shadow Quad', 8.99, N'https://d3t32hsnjxo7q6.cloudfront.net/i/c924006882e8e313d445a3a5394e4729_ra,w158,h184_pa,w158,h184.png', N'Easy to use, lots to choose!Maybelline Expert Wear Eye Shadow  Quads have 4 coordinating shades with step by step application guide  makes shadow easier than ever. The eyeshadows glide on effortlessly with  superior smoothness and the velvet-tip applicator blends without  tugging or pulling. Safe for sensitive eyes and contact lens wearers,  ophthalmologist-tested.For best results sweep the brush 4 times:Apply base color. Sweep shade on lid. Contour crease and blend. Line around eye.')
INSERT [dbo].[Product] ([ProductId], [Brand], [Name], [Price], [ImageLink], [Description]) VALUES (317, N'maybelline', N'Maybelline Fit Me Foundation with SPF', 10.99, N'https://d3t32hsnjxo7q6.cloudfront.net/i/eccb88d484b8c929fd349b0995a5dba2_ra,w158,h184_pa,w158,h184.png', N'It’s face makeup that fits you!Features: No oils, no waxes, no nonsenseNatural, Light to medium coverage that leaves skin the way it was meant to be. Fresh, breathing, flawless.Exclusive transluscent base and lightweight pigments allow skin’s natural highs and lows to show through.New shades formulated specifically for women of color contain  revolutionary 5-D pigment technology to balance your skin’s authentic  tones and highlights.SPF 18')
INSERT [dbo].[Product] ([ProductId], [Brand], [Name], [Price], [ImageLink], [Description]) VALUES (320, N'maybelline', N'Maybelline FIT ME! Matte + Poreless Foundation', 10.99, N'https://d3t32hsnjxo7q6.cloudfront.net/i/257993e12625cc45a72ec03636ffa5c5_ra,w158,h184_pa,w158,h184.jpg', N'Maybelline FIT ME! Matte + Poreless Foundation goes beyond  skin tone  matching to fit the unique texture issues of normal to oily skin for  the ultimate natural skin fit.  While some foundations can exaggerate pores and oily skin, only Maybelline''s  pore-minimizing foundation contains their genius blurring micro-powders  that erase pores and absorb oil for a naturally matte and  poreless-looking finish.Dermatologist and allergy tested.  Does not clog pores. Oil-free.     ')
INSERT [dbo].[Product] ([ProductId], [Brand], [Name], [Price], [ImageLink], [Description]) VALUES (321, N'maybelline', N'Maybelline Dream Liquid Mousse', 14.79, N'https://d3t32hsnjxo7q6.cloudfront.net/i/1ca6a4a442b9aa6b5f3d94da77d8846c_ra,w158,h184_pa,w158,h184.png', N'Airbrushed perfection made possible:Air-whipped liquid makeup for 100% poreless skin  Breakthrough finish cushions and smooths for the most flawless, luminous coverage Lightweight air-whipped formula blends to virtually eliminate pores and imperfections Innovative shade-match pump makes finding your perfect shade a dream  ')
INSERT [dbo].[Product] ([ProductId], [Brand], [Name], [Price], [ImageLink], [Description]) VALUES (339, N'maybelline', N'Maybelline Dream Wonder Liquid Touch Foundation', 14.99, N'https://d3t32hsnjxo7q6.cloudfront.net/i/ccb99ad6ac7f31a2a73454bdbda01d99_ra,w158,h184_pa,w158,h184.jpeg', N'Maybelline Dream Wonder Liquid Touch Foundation''s breakthrough texture fuses with skin. A finish so impeccable, yet undetectable.Features:Exclusive dropperMedium to full coverage, ideal for normal skinFor Best Results: Shake well. Unscrew cap. Holding dropper vertically, allow foundation to drop onto fingertip. Apply to face as usual.')
INSERT [dbo].[Product] ([ProductId], [Brand], [Name], [Price], [ImageLink], [Description]) VALUES (353, N'maybelline', N'Maybelline Superstay Better Skin Foundation', 14.99, N'https://d3t32hsnjxo7q6.cloudfront.net/i/c7d967ef502ecd79ab7ab466c4952d82_ra,w158,h184_pa,w158,h184.png', N'The Maybelline Superstay Better Skin Foundation reduces the appearance of spots, bumps, dullness and redness to give you brighter, smoother and more even skin. Features:Longwear that improves skin every minute you''re in it: brighter, smoother and more even.With micro-flex technology and the antioxidant power of Actyl-C.Ideal for sensitive skin. Good for all skin types. Medium to full coverageHow to Apply: Apply evenly to your face and blend with your fingers. ')
INSERT [dbo].[Product] ([ProductId], [Brand], [Name], [Price], [ImageLink], [Description]) VALUES (354, N'maybelline', N'Maybelline Dream Velvet Foundation', 18.49, N'https://d3t32hsnjxo7q6.cloudfront.net/i/24517c6c81c92eda31cd32b6327c1298_ra,w158,h184_pa,w158,h184.png', N'This Maybelline Dream Velvet Foundation is a refreshing gel-whipped foundation that leaves complexion perfected and smooth with a velvety, soft-matte finish. Skin stays hydrated for 12 hours. Features:Maybelline’s first hydrating matte foundation for 100% velvet-smooth perfectionUnique gel-whipped formulaIdeal for normal to combination skin, medium coverage For best results apply Maybelline Dream Velvet Foundation using Maybelline Dream Blender')
INSERT [dbo].[Product] ([ProductId], [Brand], [Name], [Price], [ImageLink], [Description]) VALUES (366, N'maybelline', N'Maybelline Mineral Power Natural Perfecting Powder Foundation', 14.99, N'https://d3t32hsnjxo7q6.cloudfront.net/i/c77ad2da76259cfd67a9a9432f635bfb_ra,w158,h184_pa,w158,h184.png', N'Why You''ll Love ItMineral Power Powder Foundation with micro-minerals provides a more natural, healthier, luminous look. Discover the natural power of micro-minerals: 100% natural mica creates a more natural luminosity Complete, yet refined coverage Provides buildable, even coverage Preservative-free, talc-free, oil-free, fragrance-free Medium to Full Coverage')
INSERT [dbo].[Product] ([ProductId], [Brand], [Name], [Price], [ImageLink], [Description]) VALUES (379, N'maybelline', N'Maybelline Dream Matte Mousse Foundation', 14.79, N'https://d3t32hsnjxo7q6.cloudfront.net/i/029889b345c76a70e8bb978b73ed1a87_ra,w158,h184_pa,w158,h184.png', N'Maybelline Dream Matte Mouse Foundation is a revolutionary air-soft mousse that provides perfecting coverage for 100% velvet-matte complexion. It''s non-comedogenic, fragrance-free, dermatologist-tested, allergy-tested and ideal for normal to oily skin.For best results: Apply smoothly and evenly to your face and blend with your fingertips.﻿   ')
INSERT [dbo].[Product] ([ProductId], [Brand], [Name], [Price], [ImageLink], [Description]) VALUES (380, N'maybelline', N'Maybelline Fit Me Shine-Free Foundation Stick', 10.99, N'https://d3t32hsnjxo7q6.cloudfront.net/i/d04e7c2ed65dabe1dca4eed9aa268e95_ra,w158,h184_pa,w158,h184.png', N'Get flawless, shine-free skin instantly and on-the-go for tailor-made  mattifying coverage.  The anti-shine core has ultra-lightweight powders  built in to the stick foundation to instantly dissolve excess oil. Features: Maybelline''s first gel stick foundation with an anti-shine core Fresh gel foundation blends to a flawless matte finish Lightweight powders in the anti-shine core instantly dissolve excess oil')
INSERT [dbo].[Product] ([ProductId], [Brand], [Name], [Price], [ImageLink], [Description]) VALUES (414, N'maybelline', N'Maybelline Dream Bouncy Blush', 11.99, N'https://d3t32hsnjxo7q6.cloudfront.net/i/51eacb9efebbaee39399e65ffe3d9416_ra,w158,h184_pa,w158,h184.png', N'Now, blush has bounce! Freshest flush ever:• New bouncy texture is formulated with silicone elastomers• Lightweight like a powder, yet melts seamlessly into skin like a cream giving you a fresh flush• Dermatologist tested• Allergy tested• Non-comedogenic                                                                                                                 For best results: With your fingertips, blend a small amount of  blush onto the apples of your cheeks, applying from your cheekbones to  your temples. To build colour intensity, apply more blush.                      ')
INSERT [dbo].[Product] ([ProductId], [Brand], [Name], [Price], [ImageLink], [Description]) VALUES (439, N'maybelline', N'Maybelline Fit Me Blush', 10.29, N'https://d3t32hsnjxo7q6.cloudfront.net/i/53d5f825461117c0d96946e1029510b0_ra,w158,h184_pa,w158,h184.png', N'Maybelline Fit Me Blush has lightweight pigments blend easily and wear evenly. It provides a natural, fade-proof cheek color that leaves skin the way it was meant to be...fresh, breathing, and natural.  For Best Results: For soft, natural look, brush along cheekbone, sweeping upward.')
INSERT [dbo].[Product] ([ProductId], [Brand], [Name], [Price], [ImageLink], [Description]) VALUES (468, N'maybelline', N'Maybelline Face Studio Master Hi-Light Light Booster Blush', 14.99, N'https://d3t32hsnjxo7q6.cloudfront.net/i/4621032a92cb428ad640c105b944b39c_ra,w158,h184_pa,w158,h184.png', N'Maybelline Face Studio Master Hi-Light Light Boosting blush formula has an expert  balance of shade + shimmer illuminator for natural glow. Skin goes  soft-lit with zero glitz.    For Best Results: Brush over all shades in palette and gently sweep over  cheekbones, brow bones, and temples, or anywhere light naturally touches  the face.                            ')
INSERT [dbo].[Product] ([ProductId], [Brand], [Name], [Price], [ImageLink], [Description]) VALUES (477, N'maybelline', N'Maybelline Facestudio Master Contour Kit', 15.99, N'https://d3t32hsnjxo7q6.cloudfront.net/i/4f731de249cbd4cb819ea7f5f4cfb5c3_ra,w158,h184_pa,w158,h184.png', N'Maybelline Facestudio Master Contour Kit is the ultimate on the go all-in-one palette, with contouring brush included.  Define and highlight in a New York minute with this effortless 3-step face contouring kit.  This easy-to-use 3-step face contouring kit features a bronzer, blush and highlighter.')
INSERT [dbo].[Product] ([ProductId], [Brand], [Name], [Price], [ImageLink], [Description]) VALUES (488, N'maybelline', N'Maybelline Fit Me Bronzer', 10.29, N'https://d3t32hsnjxo7q6.cloudfront.net/i/d4f7d82b4858c622bb3c1cef07b9d850_ra,w158,h184_pa,w158,h184.png', N'Why You''ll Love It  Lightweight pigments blend easily and wear evenly Provides a natural, fade-proof bronzed color that leaves skin the way it was meant to be...fresh, breathing and natural  For Best Results: For soft, natural look, brush along cheekbone, sweeping upward.')
INSERT [dbo].[Product] ([ProductId], [Brand], [Name], [Price], [ImageLink], [Description]) VALUES (495, N'maybelline', N'Maybelline Face Studio Master Hi-Light Light Booster Bronzer', 14.99, N'https://d3t32hsnjxo7q6.cloudfront.net/i/991799d3e70b8856686979f8ff6dcfe0_ra,w158,h184_pa,w158,h184.png', N'Maybelline Face Studio Master Hi-Light Light Boosting bronzer formula has an expert  balance of shade + shimmer illuminator for natural glow. Skin goes  soft-lit with zero glitz.    For Best Results: Brush over all shades in palette and gently sweep over  cheekbones, brow bones, and temples, or anywhere light naturally touches  the face.                            ')
USE [master]
GO
ALTER DATABASE [eShop] SET  READ_WRITE 
GO
