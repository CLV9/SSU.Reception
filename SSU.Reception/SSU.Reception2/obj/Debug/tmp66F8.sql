ALTER TABLE [dbo].[Directions] ADD [PrioritySubject] [int] NOT NULL DEFAULT 0
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202008011036476_AutomaticMigration', N'SSU.Reception.Models.EnrolleeContext',  0x1F8B0800000000000400ED5CDD6EEBB811BE2FD0771074D516593BC9E9C56E60EF22C749BA414F9C204A16EDD5012D4D6CB614E58A546063B14FB6177DA4BEC252D68FF927593F8E9DB33DC84D22CE7C1CCE8C8633D430FFFBF5BFA31F5621715E216638A263F76C70EA3A40FD28C0743E7613FEF2CDB7EE0FDFFFF10FA3EB205C393F15741F523AC149D9D85D70BEBC180E99BF8010B14188FD3862D10B1FF851384441343C3F3DFD6E7876360401E10A2CC7193D2694E310367F883F2711F561C91344EEA20008CB9F8B116F83EA4C51086C897C18BB9EF73C7884945C4832C8E85DE792602464F180BCB80EA234E2281DBF7866E0F138A2736F291E20F2B45E82A07B418441BE828B2D79D3C59C9EA78B196E190B283F613C0A5B029E7DC8B533D4D93BE9D82DB527F4772DF4CCD7E9AA373A1CBBD7348E0801B1787DB28B09895342BB8A0705E389A30C9F944E217C27FD39712609E1490C630A098F1139711E9219C1FEDF61FD14FD1BE8982684C8420A31C598F2403C7A88A325C47CFD082FB9E8B781EB0C55BEA1CE58B2493CD9A26E29FF70EE3A5331399A11287D405280C7A318FE061462C42178409C434C530CD868D1985D9BCB4B622A7E2926147E275E22D7B943AB4F40E77C3176C5AFAE73835710144F72219E2916EF9C60E271021621EB279E1E6556A11FF162AD852F1E7E6E1116A288EC366E3DCAF50A85FF0414F7437910FE1F51441E16113D82192E8300A7DE9B0BC0F62041FD8477882F3C5FBC29FDD4F698308611DD03D2C4B381EC70A00E3C3F227607012205D347E18280686B796F70CCF8277805724FD6E112A39E6E7CCF17101750ACA755445CC74BEE8988986C5D49ECAA036DA4AD8C319E63E1A313F10CBF605F04D8BDA8F121C6512C6273DF50E0811FD1A012AD9EF96981E3AEBCCA2AA64938839EE1485D8915B1C56A3AF067894228F64CD58BBADAF9D2E7F85588B21F34916AE6D9EE8F58E43FF1FACDE3E5240A5365EC7F9E297AC5F34DE652E753AEF3086443C5167899BFCC4532F75923BD89A3F03122529EA8527CF6A224F6D35737AA257B42F11C787389B3FDBC5ED482C6226336542D5C3EDE5A2AE555DA219D466B935221A99156A56B2BB5F2FED60BAD915A645628AA4556C96C128F86DB52A4B640B9C231F859F1D3B2422939BF962855731DA752F89804C21F1E88A8DF7BA627858B79C9EC5FC2D64A82628CD56237F6C722ECB474C68CEDAB271ED313AB6D9C849285B5CCF696DD1034DF1E12B534BC82B627FB8B55071093B5D0926C1F55C17790A66AF99A32C310917B2C81E32C9CFE84482286CE0CD3289C45F0CEA9CD744FA1162FDD2B26308792E1433DC33412368911F521ADBE59C9F657D37499916A0C67BCF13D4DA7E11DCD78A9D512F13A793E06EA4353CB79918F11F1781260605506B42BF692B194395DA6764EA86588EAF4D734709AA58BDB30BD3D80BC13DAC34BA12F413976FF622C6E277AE1AA12BA943DA8F067AE1E33EFE91510E0E05CFAD951ED04311F05E61E243416A84F449885388D73E91B4699B03DA6DC8CC998FA788948A34568DC0D637A2A5D398F3E72054BA069386E64A326021825B7294D39A9A6C35D2A1B0D251F6CE89AF9D6BCD36BF4F2604FCEA85515126C9133BC5B1754453FA4EFA9B66832F3F6ACF7C8DEA65678BBDDA3A2DCDB97F7D9ABC426B1F0743030B7929E0E6595E6A08E655577230733CEFE8EEB686A55BED3112A4AF43DB999BDB23F9297598539A4935975DD4400E388F8002E96153F82870B8E324B2CD6923E8715B724C9CF0CF23C99E56596EE2529AE075C451429E7B6DC325CCFF03415A3F4221B8AE4623B60B2BDC286516CC91A80A43A6341C6E9A8445C7F8EAADBB6719E5CAE47D6AAE12A8D1363094E56B09E5CA84A68A3A042AB359AB125698DD2B4CEBAD0F23209A7748F3D6A403BF7ADD3444D02D12A85E8AE197BCEF0E66EA29D33D7E8A866EF6BB3FB75D6907DBBEBABA0E220AA8CC5E5D868987538E50F46C38A56A8D11D5A2E319D4BAD51F913C7CBFAA226DF78EDDB85C20C63E8334BD750296D39138F6234076D343DAF08601380AE104733949EBC4C82D020D3779E8A505ECCA66D2EA6F18A085F30A4BF175FF76B1A982CFB758E702356977EAADB2C144C1FB2B03A698B1A2228B61CA54E229284B43A01A9E62E3B896488F261739CA901326D892037F9C838F2F316EB2A4B39656195055E35D2B6754746DA3E6DB142B57D4759A43AD41CD3ECC99161CDD1E6C852F38D0C293D6E8EA576E0C870EA4873C4B21347062B1FB6F0131B8ED71E67DBB123036D9F3647B2F5ECC898B6F1E6E87A078F8CAC8FB5B0AFFA5D4331B03AD442525B1F8F22AE8DA0A59EE53AC950725D1155E351468DAFB8D6CE13806A64A3B2938177967D0D3551B4E1546AA320E8AA11DB04768A8E9AB14D602568B107188D46CA5E608CB688DF5ACB9112BDB5B116D1D16C3D52E2A439DC063B6F325211F38726CE68A8E53CC67184915F1927D26ABED6289B93B2E7DEF9DCF654A07D4257C3FB36195DFF4C4C6DA29091D49116998FFEF954C97DF4C177E3434511DFDB81EC27130DBCA78AF1D8AEF3A606316A589DA49CBDAC65B59A7594D78FBBEFF818056546E23A4235AF38488B496FCD3884839460E0FD874C08DE44BA82E00E51FC028C67DFE1DDF3D3B373ED92D0FBB9B033642C2096FADB766B47B5D7013A8170AAD59DBD3E6D7B94D59B32F415C5FE02C57F0AD1EACFBD6EBFF443326FB4F4C3D36FA96C74691CB7DFD2005663F7E70DD38573FB8FCF05DF89731F0B5FBE704E9D5F7A5F6ED94CBE8FAB2DFD7452755DA512B5D395942E8BB55D48E982A35D47D1209A2CC7EB8DA05F5599E1F6EBA8BEA6D2452BF64B2A9DEC64BBA2D249A4EA3B299DF565DE0269FAD26BECBDDEFDAADB2C8D0390C65F254C1357ACB81CD354148DBD8F2435576DBAB84FDD459B0EAF6CCDB59B0E68559770BA38B6FD024E17A4CACB37BDA2BF76C1A60556874B09BF8F046C7F3993ADB7BF53AA61EFECDF0DD5B691FFFFDB8055DAEADD002CF5681CBC1DD7FC8EDBB1FFA9537752DD39D49B7522FD0EDB758B5E92C3F6D11ED7772A8FA08EEB385F58CB6DCBD8636D4CFC1A432A5CE18B6C8EFDEA116FE711EFA795D5EC3BAAF880A09FA2D635AA6647CD63379845C2DE5922558C9A3D5F957DACF56DACB6496A3ACBEC6DAE355DAE36FCAAE6C743B7C01A2AD2BEC835E9D3B375F85976FBE336B6AA5692BF1CBD8F25BE41E7EA1761DBFD77A3BE8765B7E83135BFC389D02CFD473EB13F303CDF42A4FF9F8F664BD9821634B7F4252A36094DA282443FAD078E0211B12FD36357E48B9811F9C0D8E60A787E69F53A9C41704BEF13BE4CB858328433A2F443A47B4CDDFC9B465A55E6D1FDE6EB2FDBC7128498582C01EEE9C70493A094FBC652715740A49B577E2090DA92A70703F3758934355A0EAB8072F5957BEE13844B22C0D83DF5D02B7491ED99C12798237F5D7C4EAD06D96D0855EDA32B8CE6310A598EB1E5177F0A1F0EC2D5F7BF0163722EF798520000 , N'6.2.0-61023')
