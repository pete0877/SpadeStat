/*==============================================================*/
/* Table: NextID                                                */
/*==============================================================*/
create table NextID (
NextID               INT4                 not null
);

/*==============================================================*/
/* Table: Player                                                */
/*==============================================================*/
create table Player (
PlayerId             INT4                 not null,
PlayerNm             VARCHAR(100)         null,
LocationTxt          VARCHAR(100)         null,
PlayerNoteTxt        VARCHAR(1000)        null,
PlayerTypCd          VARCHAR(50)          null,
constraint PK_PLAYER primary key (PlayerId)
);

/*==============================================================*/
/* Index: IX_PLAYERNM                                           */
/*==============================================================*/
create unique index IX_PLAYERNM on Player (
PlayerNm
);

/*==============================================================*/
/* Table: PlayerBankroll                                        */
/*==============================================================*/
create table PlayerBankroll (
BankrollId           INT4                 not null,
PlayerId             INT4                 null,
PlayerNm             VARCHAR(100)         null,
StartDt              TIMESTAMP            null,
EndDt                TIMESTAMP            null,
NetChangeAmt         DECIMAL(20,2)        null,
NetTimeAmt           DECIMAL(20,2)        null,
TypeCd               VARCHAR(50)          null,
SessionNoteTxt       VARCHAR(255)         null,
HandCt               INT4                 null,
GameTypCd            VARCHAR(20)          null,
BetTypCd             VARCHAR(10)          null,
BBAmt                DECIMAL(13,2)        null,
SiteCd               VARCHAR(50)          null,
constraint PK_PLAYERBANKROLL primary key (BankrollId)
);

/*==============================================================*/
/* Index: IX_PLAYERBANKROLL_PLAYERID                            */
/*==============================================================*/
create  index IX_PLAYERBANKROLL_PLAYERID on PlayerBankroll (
PlayerId
);

/*==============================================================*/
/* Index: IX_PLAYERBANKROLL_PLAYERNM                            */
/*==============================================================*/
create  index IX_PLAYERBANKROLL_PLAYERNM on PlayerBankroll (
PlayerNm
);

/*==============================================================*/
/* Table: Report                                                */
/*==============================================================*/
create table Report (
ReportID             INT4                 not null,
ReportNm             VARCHAR(50)          null,
ReportPage           VARCHAR(50)          null,
constraint PK_REPORT primary key (ReportID)
);

/*==============================================================*/
/* Table: ReportSection                                         */
/*==============================================================*/
create table ReportSection (
SectionId            INT4                 not null,
ReportId             INT4                 null,
SectionNm            VARCHAR(50)          null,
SectionSQL           VARCHAR(7000)        null,
SectionPage          VARCHAR(255)         null,
CritHandComboFlg     INT2                 null,
CritPositionFlg      INT2                 null,
CritNumPlayerFlg     INT2                 null,
CritGameTypFlg       INT2                 null,
CritBetTypFlg        INT2                 null,
CritTournamentTypFlg INT2                 null,
CritPlayerNmFlg      INT2                 null,
CritDateRangeFlg     INT2                 null,
CritLimitRangeFlg    INT2                 null,
CritTableTypeFlg     INT2                null,
CritTounamendIdFlg   INT2                 null,
CritHandIdFlg        INT2                 null,
OrderBy              VARCHAR(1000)        null,
constraint PK_REPORTSECTION primary key (SectionId)
);

/*==============================================================*/
/* Index: IX_REPORTSECTION_REPORTID                             */
/*==============================================================*/
create  index IX_REPORTSECTION_REPORTID on ReportSection (
ReportId
);

/*==============================================================*/
/* Table: Tournament                                            */
/*==============================================================*/
create table Tournament (
TournamentId         INT4                 not null,
StartDt              TIMESTAMP            null,
EndDt                TIMESTAMP            null,
TournamentTypCd      VARCHAR(10)          null,
GameTypCd            VARCHAR(20)          null,
BuyInAmt             DECIMAL(20,2)        null,
FeeAmt               DECIMAL(20,2)        null,
RebuyFlg             INT2                 null,
RebuySpecTxt         VARCHAR(255)         null,
BetTypCd             VARCHAR(10)          null,
TotalPlayerNum       INT4                 null,
TotalPrizeFundAmt    DECIMAL(20,2)        null,
NoteTxt              VARCHAR(1000)        null,
constraint PK_TOURNAMENT primary key (TournamentId)
);

/*==============================================================*/
/* Table: TournamentHand                                        */
/*==============================================================*/
create table TournamentHand (
HandId               INT4                 not null,
TournamentId         INT4                 null,
GameTypCd            VARCHAR(20)          null,
BigBlindAmt          DECIMAL(20,2)        null,
SmallBlindAmt        DECIMAL(20,2)        null,
BigBetAmt            DECIMAL(20,2)        null,
SmallBetAmt          DECIMAL(20,2)        null,
AnteAmt              DECIMAL(20,2)        null,
ButtonSeatNum        INT4                 null,
StartDt              TIMESTAMP            null,
TableId              VARCHAR(50)          null,
PotAmt               DECIMAL(20,2)        null,
RakeAmt              DECIMAL(20,2)        null,
BetTypCd             VARCHAR(10)          null,
TotalPlayerNum       INT4                 null,
AceFlg               INT2                 null,
DeuceFlg             INT2                 null,
TreyFlg              INT2                 null,
FourFlg              INT2                 null,
FiveFlg              INT2                 null,
SixFlg               INT2                 null,
SevenFlg             INT2                 null,
EightFlg             INT2                 null,
NineFlg              INT2                 null,
TenFlg               INT2                 null,
JackFlg              INT2                 null,
QueenFlg             INT2                 null,
KingFlg              INT2                 null,
StealAttemptFlg      INT2                 null,
FinalRoundCd         VARCHAR(20)          null,
NoteTxt              VARCHAR(1000)        null,
constraint PK_TOURNAMENTHAND primary key (HandId)
);

/*==============================================================*/
/* Index: IX_TOURNAMENTHAND_TOURNAMENTID                        */
/*==============================================================*/
create  index IX_TOURNAMENTHAND_TOURNAMENTID on TournamentHand (
TournamentId
);

/*==============================================================*/
/* Index: IX_TOURNAMENTHAND_ACEFLG                              */
/*==============================================================*/
create  index IX_TOURNAMENTHAND_ACEFLG on TournamentHand (
AceFlg
);

/*==============================================================*/
/* Index: IX_TOURNAMENTHAND_DEUCEFLG                            */
/*==============================================================*/
create  index IX_TOURNAMENTHAND_DEUCEFLG on TournamentHand (
DeuceFlg
);

/*==============================================================*/
/* Index: IX_TOURNAMENTHAND_TREYFLG                             */
/*==============================================================*/
create  index IX_TOURNAMENTHAND_TREYFLG on TournamentHand (
TreyFlg
);

/*==============================================================*/
/* Index: IX_TOURNAMENTHAND_FOURFLG                             */
/*==============================================================*/
create  index IX_TOURNAMENTHAND_FOURFLG on TournamentHand (
FourFlg
);

/*==============================================================*/
/* Index: IX_TOURNAMENTHAND_FIVEFLG                             */
/*==============================================================*/
create  index IX_TOURNAMENTHAND_FIVEFLG on TournamentHand (
FiveFlg
);

/*==============================================================*/
/* Index: IX_TOURNAMENTHAND_SIXFLG                              */
/*==============================================================*/
create  index IX_TOURNAMENTHAND_SIXFLG on TournamentHand (
SixFlg
);

/*==============================================================*/
/* Index: IX_TOURNAMENTHAND_SEVENFLG                            */
/*==============================================================*/
create  index IX_TOURNAMENTHAND_SEVENFLG on TournamentHand (
SevenFlg
);

/*==============================================================*/
/* Index: IX_TOURNAMENTHAND_EIGHTFLG                            */
/*==============================================================*/
create  index IX_TOURNAMENTHAND_EIGHTFLG on TournamentHand (
EightFlg
);

/*==============================================================*/
/* Index: IX_TOURNAMENTHAND_NINEFLG                             */
/*==============================================================*/
create  index IX_TOURNAMENTHAND_NINEFLG on TournamentHand (
NineFlg
);

/*==============================================================*/
/* Index: IX_TOURNAMENTHAND_TENFLG                              */
/*==============================================================*/
create  index IX_TOURNAMENTHAND_TENFLG on TournamentHand (
TenFlg
);

/*==============================================================*/
/* Index: IX_TOURNAMENTHAND_JACKFLG                             */
/*==============================================================*/
create  index IX_TOURNAMENTHAND_JACKFLG on TournamentHand (
JackFlg
);

/*==============================================================*/
/* Index: IX_TOURNAMENTHAND_QUEENFLG                            */
/*==============================================================*/
create  index IX_TOURNAMENTHAND_QUEENFLG on TournamentHand (
QueenFlg
);

/*==============================================================*/
/* Index: IX_TOURNAMENTHAND_KINGFLG                             */
/*==============================================================*/
create  index IX_TOURNAMENTHAND_KINGFLG on TournamentHand (
KingFlg
);

/*==============================================================*/
/* Index: IX_TOURNAMENTHAND_STEALATTEMPTF                       */
/*==============================================================*/
create  index IX_TOURNAMENTHAND_STEALATTEMPTF on TournamentHand (
StealAttemptFlg
);

/*==============================================================*/
/* Index: IX_TOURNAMENTHAND_FINALBETTINGR                       */
/*==============================================================*/
create  index IX_TOURNAMENTHAND_FINALBETTINGR on TournamentHand (
FinalRoundCd
);

/*==============================================================*/
/* Table: TournamentPlayer                                      */
/*==============================================================*/
create table TournamentPlayer (
TournamentPlayerId   INT4                 not null,
TournamentId         INT4                 null,
PlayerId             INT4                 null,
PlayerNm             VARCHAR(100)         null,
PlacementNum         INT4                 null,
WinningAmt           DECIMAL(20,2)        null,
SatelliteSeatFlg     INT2                 null,
constraint PK_TOURNAMENTPLAYER primary key (TournamentPlayerId)
);

/*==============================================================*/
/* Index: IX_TOURNAMENTPLAYER_TOURNAMENTI                       */
/*==============================================================*/
create  index IX_TOURNAMENTPLAYER_TOURNAMENTI on TournamentPlayer (
TournamentId
);

/*==============================================================*/
/* Index: IX_TOURNAMENTPLAYER_PLAYERID                          */
/*==============================================================*/
create  index IX_TOURNAMENTPLAYER_PLAYERID on TournamentPlayer (
PlayerId
);

/*==============================================================*/
/* Index: IX_TOURNAMENTPLAYER_PLAYERNM                          */
/*==============================================================*/
create  index IX_TOURNAMENTPLAYER_PLAYERNM on TournamentPlayer (
PlayerNm
);

/*==============================================================*/
/* Table: HandPlayer                                            */
/*==============================================================*/
create table HandPlayer (
HandPlayerId         INT4                 not null,
PlayerId             INT4                 null,
PlayerNm             VARCHAR(100)         null,
HandId               INT4                 null,
TournamentId         INT4                 null,
PlayerSeatNum        INT4                 null,
HandStartChipAmt     DECIMAL(20,2)        null,
GameTypCd            VARCHAR(20)          null,
BetTypCd             VARCHAR(10)          null,
FirstInFlg           INT2                 null,
PreFlopRaiseFlg      INT2                 null,
AceFlg               INT2                 null,
DeuceFlg             INT2                 null,
TreyFlg              INT2                 null,
FourFlg              INT2                 null,
FiveFlg              INT2                 null,
SixFlg               INT2                 null,
SevenFlg             INT2                 null,
EightFlg             INT2                 null,
NineFlg              INT2                 null,
TenFlg               INT2                 null,
JackFlg              INT2                 null,
QueenFlg             INT2                 null,
KingFlg              INT2                 null,
SuitedStsCd          VARCHAR(2)           null,
PositionNum          INT4                 null,
MoneyInAmt           DECIMAL(20,2)        null,
MoneyWonAmt          DECIMAL(20,2)        null,
VoluntaryMoneyFlg    INT2                 null,
BlindStsCd           VARCHAR(2)           null,
FoldCd               VARCHAR(20)          null,
constraint PK_HANDPLAYER primary key (HandPlayerId)
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_PLAYERID                                */
/*==============================================================*/
create  index IX_HANDPLAYER_PLAYERID on HandPlayer (
PlayerId
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_PLAYERNM                                */
/*==============================================================*/
create  index IX_HANDPLAYER_PLAYERNM on HandPlayer (
PlayerNm
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_HANDID                                  */
/*==============================================================*/
create  index IX_HANDPLAYER_HANDID on HandPlayer (
HandId
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_TOURNAMENTID                            */
/*==============================================================*/
create  index IX_HANDPLAYER_TOURNAMENTID on HandPlayer (
TournamentId
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_ACEFLG                                  */
/*==============================================================*/
create  index IX_HANDPLAYER_ACEFLG on HandPlayer (
AceFlg
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_DEUCEFLG                                */
/*==============================================================*/
create  index IX_HANDPLAYER_DEUCEFLG on HandPlayer (
DeuceFlg
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_TREYFLG                                 */
/*==============================================================*/
create  index IX_HANDPLAYER_TREYFLG on HandPlayer (
TreyFlg
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_FOURFLG                                 */
/*==============================================================*/
create  index IX_HANDPLAYER_FOURFLG on HandPlayer (
FourFlg
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_FIVEFLG                                 */
/*==============================================================*/
create  index IX_HANDPLAYER_FIVEFLG on HandPlayer (
FiveFlg
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_SIXFLG                                  */
/*==============================================================*/
create  index IX_HANDPLAYER_SIXFLG on HandPlayer (
SixFlg
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_SEVENFLG                                */
/*==============================================================*/
create  index IX_HANDPLAYER_SEVENFLG on HandPlayer (
SevenFlg
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_EIGHTFLG                                */
/*==============================================================*/
create  index IX_HANDPLAYER_EIGHTFLG on HandPlayer (
EightFlg
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_NINEFLG                                 */
/*==============================================================*/
create  index IX_HANDPLAYER_NINEFLG on HandPlayer (
NineFlg
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_TENFLG                                  */
/*==============================================================*/
create  index IX_HANDPLAYER_TENFLG on HandPlayer (
TenFlg
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_JACKFLG                                 */
/*==============================================================*/
create  index IX_HANDPLAYER_JACKFLG on HandPlayer (
JackFlg
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_QUEENFLG                                */
/*==============================================================*/
create  index IX_HANDPLAYER_QUEENFLG on HandPlayer (
QueenFlg
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_KINGFLG                                 */
/*==============================================================*/
create  index IX_HANDPLAYER_KINGFLG on HandPlayer (
KingFlg
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_SUITEDSTSCD                             */
/*==============================================================*/
create  index IX_HANDPLAYER_SUITEDSTSCD on HandPlayer (
SuitedStsCd
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_FIRSTINFLG                              */
/*==============================================================*/
create  index IX_HANDPLAYER_FIRSTINFLG on HandPlayer (
FirstInFlg
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_PREFLOPRAISEFLG                         */
/*==============================================================*/
create  index IX_HANDPLAYER_PREFLOPRAISEFLG on HandPlayer (
PreFlopRaiseFlg
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_POSITIONNUM                             */
/*==============================================================*/
create  index IX_HANDPLAYER_POSITIONNUM on HandPlayer (
PositionNum
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_BLINDSTSCD                              */
/*==============================================================*/
create  index IX_HANDPLAYER_BLINDSTSCD on HandPlayer (
BlindStsCd
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_VOLUNTARYMONEYFLG                       */
/*==============================================================*/
create  index IX_HANDPLAYER_VOLUNTARYMONEYFLG on HandPlayer (
VoluntaryMoneyFlg
);

/*==============================================================*/
/* Index: IX_HANDPLAYER_FOLDCD                                  */
/*==============================================================*/
create  index IX_HANDPLAYER_FOLDCD on HandPlayer (
FoldCd
);

/*==============================================================*/
/* Table: HandPlayerAction                                      */
/*==============================================================*/
create table HandPlayerAction (
ActionId             INT4                 not null,
HandId               INT4                 null,
PlayerId             INT4                 null,
PlayerNm             VARCHAR(100)         null,
HandPlayerId         INT4                 null,
BettingRoundTypCd    VARCHAR(20)          null,
BetOrderNum          INT4                 null,
ActionTypCd          VARCHAR(20)          null,
ActionAmt            DECIMAL(20,2)        null,
constraint PK_HANDPLAYERACTION primary key (ActionId)
);

/*==============================================================*/
/* Index: IX_HANDPLAYERACTION_HANDID                            */
/*==============================================================*/
create  index IX_HANDPLAYERACTION_HANDID on HandPlayerAction (
HandId
);

/*==============================================================*/
/* Index: IX_HANDPLAYERACTION_PLAYERID                          */
/*==============================================================*/
create  index IX_HANDPLAYERACTION_PLAYERID on HandPlayerAction (
PlayerId
);

/*==============================================================*/
/* Index: IX_HANDPLAYERACTION_PLAYERNM                          */
/*==============================================================*/
create  index IX_HANDPLAYERACTION_PLAYERNM on HandPlayerAction (
PlayerNm
);

/*==============================================================*/
/* Index: IX_HANDPLAYERACTION_HANDPLAYERI                       */
/*==============================================================*/
create  index IX_HANDPLAYERACTION_HANDPLAYERI on HandPlayerAction (
HandPlayerId
);

/*==============================================================*/
/* Index: IX_HANDPLAYERACTION_BETTINGROUN                       */
/*==============================================================*/
create  index IX_HANDPLAYERACTION_BETTINGROUN on HandPlayerAction (
BettingRoundTypCd
);

/*==============================================================*/
/* Table: HandPlayerCombo                                       */
/*==============================================================*/
create table HandPlayerCombo (
ComboId              INT4                 not null,
HandPlayerId         INT4                 null,
HandComboTxt         VARCHAR(4)           null,
constraint PK_HANDPLAYERCOMBO primary key (ComboId)
);

/*==============================================================*/
/* Index: IX_COMBO_HANDPLAYERID                                 */
/*==============================================================*/
create  index IX_COMBO_HANDPLAYERID on HandPlayerCombo (
HandPlayerId
);

/*==============================================================*/
/* Index: IX_COMBO_HANDCOMBOTXT                                 */
/*==============================================================*/
create  index IX_COMBO_HANDCOMBOTXT on HandPlayerCombo (
HandComboTxt
);

/*==============================================================*/
/* Table: HandCard                                              */
/*==============================================================*/
create table HandCard (
CardId               INT4                 not null,
HandPlayerId         INT4                 null,
PlayerId             INT4                 null,
PlayerNm             VARCHAR(100)         null,
HandId               INT4                 null,
CardValTxt           CHAR(1)              null,
CardSuitTxt          CHAR(1)              null,
TypCd                CHAR(2)              null,
constraint PK_HANDCARD primary key (CardId)
);

/*==============================================================*/
/* Index: IX_HANDCARD_HANDPLAYERID                              */
/*==============================================================*/
create  index IX_HANDCARD_HANDPLAYERID on HandCard (
HandPlayerId
);

/*==============================================================*/
/* Index: IX_HANDCARD_PLAYERID                                  */
/*==============================================================*/
create  index IX_HANDCARD_PLAYERID on HandCard (
PlayerId
);

/*==============================================================*/
/* Index: IX_HANDCARD_PLAYERNM                                  */
/*==============================================================*/
create  index IX_HANDCARD_PLAYERNM on HandCard (
PlayerNm
);

/*==============================================================*/
/* Index: IX_HANDCARD_HANDID                                    */
/*==============================================================*/
create  index IX_HANDCARD_HANDID on HandCard (
HandId
);

/*==============================================================*/
/* View: HandSummaryView                                        */
/*==============================================================*/
create view HandSummaryView as
SELECT     th.TournamentId, 
           th.HandId, 
           hp.PlayerId, 
           hp.HandPlayerId, 
           hp.PlayerNm, 
           th.SmallBlindAmt, 
           th.BigBlindAmt, 
           th.SmallBetAmt, 
           th.BigBetAmt, 
           th.BetTypCd,
           th.GameTypCd, 
           hp.PlayerSeatNum, 
           th.ButtonSeatNum, 
           th.StartDt, 
           th.AnteAmt, 
           th.TableId, 
           th.PotAmt,
           th.RakeAmt,
           hp.AceFlg AS HoleAce, 
           hp.DeuceFlg AS HoleDeuce, 
           hp.TreyFlg AS HoleTrey, 
           hp.FourFlg AS HoleFour, 
           hp.FiveFlg AS HoleFive,
           hp.SixFlg AS HoleSix, 
           hp.SevenFlg AS HoleSeven, 
           hp.EightFlg AS HoleEight, 
           hp.NineFlg AS HoleNine, 
           hp.TenFlg AS HoleTen, 
           hp.JackFlg AS HoleJack, 
           hp.QueenFlg AS HoleQueen, 
           hp.KingFlg AS HoleKing,
           hp.SuitedStsCd, 
           hpa.BettingRoundTypCd, 
           hpa.BetOrderNum, 
           hpa.ActionTypCd, 
           hpa.ActionAmt, 
           hp.FirstInFlg, 
           hp.VoluntaryMoneyFlg,
           hp.PreFlopRaiseFlg, 
           hp.PositionNum, 
           hp.BlindStsCd,
           th.TotalPlayerNum,
           th.AceFlg AS BoardAce, 
           th.DeuceFlg AS BoardDeuce, 
           th.TreyFlg AS BoardTrey, 
           th.FourFlg AS BoardFour, 
           th.FiveFlg AS BoardFive,
           th.SixFlg AS BoardSix, 
           th.SevenFlg AS BoardSeven, 
           th.EightFlg AS BoardEight, 
           th.NineFlg AS BoardNine, 
           th.TenFlg AS BoardTen, 
           th.JackFlg AS BoardJack, 
           th.QueenFlg AS BoardQueen, 
           th.KingFlg AS BoardKing
FROM         TournamentHand th INNER JOIN
             HandPlayer hp ON th.HandId = hp.HandId INNER JOIN
             HandPlayerAction hpa ON hp.HandPlayerId = hpa.HandPlayerId;

/*==============================================================*/
/* View: RptFlopSummary                                         */
/*==============================================================*/
create view RptFlopSummary as
SELECT hp.PlayerId, hp.PlayerNm, hp.GameTypCd, hp.BetTypCd,
       COUNT(hp.HandId)    AS TotalHandsPlayed,
       SUM(CASE WHEN hp.BlindStsCd = 'SB' THEN 1 ELSE 0 END) AS TotalHandsFromSB,
       SUM(CASE WHEN hp.BlindStsCd = 'SB' AND hp.FoldCd <> 'PreFlop' THEN 1 ELSE 0 END)  AS FlopsFromSB,
       SUM(CASE WHEN hp.BlindStsCd = 'BB' THEN 1 ELSE 0 END)  AS TotalHandsFromBB,
       SUM(CASE WHEN hp.BlindStsCd = 'BB' AND hp.FoldCd <> 'PreFlop' THEN 1 ELSE 0 END)  AS FlopsFromBB,
       SUM(CASE WHEN hp.BlindStsCd IS NULL THEN 1 ELSE 0 END)  AS TotalHandsFromOtherPos,
       SUM(CASE WHEN hp.BlindStsCd IS NULL AND hp.FoldCd <> 'PreFlop' THEN 1 ELSE 0 END)  AS FlopsFromOtherPos,
       SUM(CASE WHEN hp.FoldCd <> 'PreFlop' THEN 1 ELSE 0 END)  AS FlopsFromAllPos,
       SUM(CASE WHEN th.FinalRoundCd = 'Show Down' AND hp.MoneyWonAmt > 0 THEN 1 ELSE 0 END) AS PotsWonAtShowdown,
       SUM(CASE WHEN th.FinalRoundCd = 'Show Down' AND hp.MoneyWonAmt = 0 THEN 1 ELSE 0 END) AS NumShowdown,
       SUM(CASE WHEN th.FinalRoundCd <> 'Show Down' AND hp.MoneyWonAmt > 0 THEN 1 ELSE 0 END) AS PotsWonNoShowdown
FROM HandPlayer hp
INNER JOIN TournamentHand th ON hp.HandId = th.HandId
WHERE hp.GameTypCd IN ('Hold''em', 'Omaha', 'Omaha Hi/Lo')
GROUP BY hp.PlayerId, hp.PlayerNm, hp.GameTypCd, hp.BetTypCd;

/*==============================================================*/
/* View: RptLimitSummary                                        */
/*==============================================================*/
create view RptLimitSummary as
SELECT th.SmallBlindAmt, th.BigBlindAmt, th.SmallBetAmt, th.BigBetAmt, COUNT(th.HandId) AS CtTotalHand,
SUM(hp.VoluntaryMoneyFlg)/COUNT(th.HandId) AS PctVoluntaryMoneyInPot,
SUM(hp.VoluntaryMoneyFlg)/SUM(CASE WHEN hp.BlindStsCd = 'SB' THEN 1 ELSE 0 END) AS PctVoluntaryMoneySB,
SUM(CASE WHEN hp.BlindStsCd = 'SB' AND hp.FoldCd = 'PreFlop' AND th.StealAttemptFlg = 1 THEN 1 ELSE 0 END)/SUM(CASE WHEN hp.BlindStsCd = 'SB' THEN 1 ELSE 0 END) AS PctFoldSBToSteal,
SUM(CASE WHEN hp.BlindStsCd = 'BB' AND hp.FoldCd = 'PreFlop' AND th.StealAttemptFlg = 1 THEN 1 ELSE 0 END)/SUM(CASE WHEN hp.BlindStsCd = 'BB' THEN 1 ELSE 0 END) AS PctFoldBBToSteal,
SUM(CASE WHEN hp.PositionNum IN (1, 2) AND hp.FirstInFlg = 1 AND hp.PreFlopRaiseFlg = 1 THEN 1 ELSE 0 END)/COUNT(th.HandId) AS PctStealBlind,
SUM(CASE WHEN hp.MoneyWonAmt > 0 AND th.FinalRoundCd = 'PreFlop' THEN 1 ELSE 0 END)/COUNT(th.HandId) AS PctWonNoFlop,
SUM(hp.MoneyWonAmt) AS SumWinningAmt,
((SUM(hp.MoneyWonAmt) - SUM(hp.MoneyInAmt))/th.BigBetAmt)/COUNT(th.HandId) AS BigBetPerHand,
SUM(CASE WHEN th.FinalRoundCd = 'Showdown' AND hp.FoldCd IS NULL THEN 1 ELSE 0 END)/COUNT(th.HandId) AS PctShowdown,
SUM(CASE WHEN th.FinalRoundCd = 'Showdown' AND hp.MoneyWonAmt > 0 THEN 1 ELSE 0 END)/Count(th.HandId) AS PctWonShowdown,
SUM(PreFlopRaiseFlg)/COUNT(th.HandId) AS PctPreFlopRaise
FROM TournamentHand th
INNER JOIN HandPlayer hp ON th.HandId = hp.HandId
GROUP BY SmallBlindAmt, BigBlindAmt, SmallBetAmt, BigBetAmt;

/*==============================================================*/
/* View: RptStartHandGeneral                                    */
/*==============================================================*/
create view RptStartHandGeneral as
SELECT hp.playerid, hp.playernm, hp.gametypcd, hp.bettypcd, hpc.HandComboTxt, 
       hp.SuitedStsCd, 
       COUNT(hp.HandId) AS CtTimes,
       ROUND((SUM(CAST(CASE WHEN hp.MoneyWonAmt > 0 THEN 1 ELSE 0 END AS DECIMAL(13,2)))/COUNT(hp.HandId))*100, 3) AS PctWinAmt,
       SUM(hp.MoneyWonAmt) AS SumTotalWin,
       SUM(hp.MoneyWonAmt) - SUM(hp.MoneyInAmt) AS NetWinAmt,
       (SUM(hp.MoneyWonAmt) - SUM(hp.MoneyInAmt))/COUNT(hp.HandId) AS AvgWinAmt,
       (SUM((hp.MoneyWonAmt - hp.MoneyInAmt)/CASE WHEN th.BigBetAmt = 0 THEN th.BigBlindAmt ELSE th.BigBetAmt END)/COUNT(hp.HandId)) AS AvgBBPerHand,
       SUM(CASE WHEN hp.BlindStsCd IS NOT NULL THEN 1 ELSE 0 END) AS CtBlindTimes,
       ROUND((SUM(CAST(CASE WHEN hp.VoluntaryMoneyFlg = 1 THEN 1 ELSE 0 END AS DECIMAL(13,2)))/COUNT(hp.HandId))*100, 3) AS PctVoluntaryMoneyInPot,
       ROUND((SUM(CAST(CASE WHEN hp.MoneyWonAmt > 0 AND th.FinalRoundCd = 'PreFlop' THEN 1 ELSE 0 END AS DECIMAL(13,2)))/COUNT(hp.HandId))*100, 3) AS PctMoneyWonNoFlop,
       ROUND((SUM(CAST(CASE WHEN hp.PreFlopRaiseFlg = 1 THEN 1 ELSE 0 END AS DECIMAL(13,2)))/COUNT(hp.HandId))*100, 3) AS PctPreFlopRaise,
       ROUND((SUM(CAST(CASE WHEN hp.PreFlopRaiseFlg = 1 AND hp.FirstInFlg = 1 THEN 1 ELSE 0 END AS DECIMAL(13,2)))/COUNT(hp.HandId))*100, 3) AS PctRaiseFirstIn,
       ROUND((SUM(CAST(CASE WHEN th.FinalRoundCd = 'Show Down' AND hp.FoldCd IS NULL THEN 1 ELSE 0 END AS DECIMAL(13,2)))/COUNT(hp.HandId))*100, 3) AS PctWentToShowdown,
       ROUND((SUM(CAST(CASE WHEN th.FinalRoundCd = 'Show Down' AND MoneyWonAmt > 0 THEN 1 ELSE 0 END AS DECIMAL(13,2)))/COUNT(hp.HandId))*100, 3) AS PctWonShowdown
FROM HandPlayer hp
INNER JOIN TournamentHand th ON hp.HandId = th.HandId
INNER JOIN HandPlayerCombo hpc ON hp.HandPlayerId = hpc.HandPlayerId
GROUP BY hp.playerid, hp.playernm, hp.gametypcd, hp.bettypcd, hpc.HandComboTxt, hp.SuitedStsCd;

/*==============================================================*/
/* View: RptStartHandGeneral2                                   */
/*==============================================================*/
create view RptStartHandGeneral2 as
SELECT     hp.playerid, hp.playernm, th.gametypcd, th.bettypcd, hpc.HandComboTxt, hp.SuitedStsCd, hp.positionnum, th.totalplayernum,
COUNT(hp.HandId) AS CtTimes, 
(SUM(CAST(CASE WHEN hp.MoneyWonAmt > 0 THEN 1 ELSE 0 END AS DECIMAL(13, 2))) / COUNT(hp.HandId)) * 100 AS PctWinAmt, 
SUM(hp.MoneyWonAmt) AS SumTotalWin, 
SUM(hp.MoneyWonAmt) - SUM(hp.MoneyInAmt) AS NetWinAmt, 
(SUM(hp.MoneyWonAmt) - SUM(hp.MoneyInAmt)) / COUNT(hp.HandId) AS AvgWinAmt, 
CAST((SUM((hp.MoneyWonAmt - hp.MoneyInAmt) / CASE WHEN th.BigBetAmt = 0 THEN th.BigBlindAmt ELSE th.BigBetAmt END) / COUNT(hp.HandId)) AS DECIMAL(13,2)) AS AvgBBPerHand, 
SUM(CASE WHEN hp.BlindStsCd IS NOT NULL THEN 1 ELSE 0 END) AS CtBlindTimes, 
(SUM(CAST(CASE WHEN hp.VoluntaryMoneyFlg = 1 THEN 1 ELSE 0 END AS DECIMAL(13, 2))) / COUNT(hp.HandId)) * 100 AS PctVoluntaryMoneyInPot, 
(SUM(CAST(CASE WHEN hp.MoneyWonAmt > 0 AND th.FinalRoundCd = 'PreFlop' THEN 1 ELSE 0 END AS DECIMAL(13, 2))) / COUNT(hp.HandId)) * 100 AS PctMoneyWonNoFlop,
(SUM(CAST(CASE WHEN hp.PreFlopRaiseFlg = 1 THEN 1 ELSE 0 END AS DECIMAL(13, 2))) / COUNT(hp.HandId)) * 100 AS PctPreFlopRaise,
(SUM(CAST(CASE WHEN hp.PreFlopRaiseFlg = 1 AND hp.FirstInFlg = 1 THEN 1 ELSE 0 END AS DECIMAL(13, 2))) / COUNT(hp.HandId)) * 100 AS PctRaiseFirstIn, 
(SUM(CAST(CASE WHEN th.FinalRoundCd = 'Show down' AND hp.FoldCd IS NULL THEN 1 ELSE 0 END AS DECIMAL(13, 2))) / COUNT(hp.HandId)) * 100 AS PctWentToShowdown, 
(SUM(CAST(CASE WHEN th.FinalRoundCd = 'Show down' AND MoneyWonAmt > 0 THEN 1 ELSE 0 END AS DECIMAL(13, 2))) / COUNT(hp.HandId)) * 100 AS PctWonShowdown
FROM         HandPlayer hp INNER JOIN
                      TournamentHand th ON hp.HandId = th.HandId INNER JOIN
                      HandPlayerCombo hpc ON hp.HandPlayerId = hpc.HandPlayerId
GROUP BY hp.playerid, hp.playernm, th.gametypcd, th.bettypcd, hpc.HandComboTxt, hp.SuitedStsCd, hp.positionnum, th.totalplayernum;

/*==============================================================*/
/* View: RptStudSummary                                         */
/*==============================================================*/
create view RptStudSummary as
SELECT hp.PlayerNm, hp.GameTypCd, hp.BetTypCd,
       COUNT(hp.HandId)    AS TotalHandsPlayed,
       SUM(CASE WHEN FoldCd NOT IN ('3rd Street', '4th Street') THEN 1 ELSE 0 END) AS Saw4thStreet,
       SUM(CASE WHEN FoldCd NOT IN ('3rd Street', '4th Street', '5th Street') THEN 1 ELSE 0 END) AS Saw5thStreet,
       SUM(CASE WHEN FoldCd NOT IN ('3rd Street', '4th Street', '5th Street', '6th Street') THEN 1 ELSE 0 END) AS Saw6thStreet,
       SUM(CASE WHEN FoldCd NOT IN ('3rd Street', '4th Street', '5th Street', '6th Street', 'River') THEN 1 ELSE 0 END) AS Saw7thStreet,
       SUM(CASE WHEN th.FinalRoundCd = 'Show Down' AND MoneyWonAmt > 0 THEN 1 ELSE 0 END) AS PotsWonAtShowdown,
       SUM(CASE WHEN th.FinalRoundCd = 'Show Down' AND MoneyWonAmt = 0 THEN 1 ELSE 0 END) AS NumShowdown,
       SUM(CASE WHEN th.FinalRoundCd <> 'Show Down' AND MoneyWonAmt > 0 THEN 1 ELSE 0 END) AS PotsWonNoShowdown
FROM HandPlayer hp
INNER JOIN TournamentHand th ON hp.HandId = th.HandId
WHERE hp.GameTypCd IN ('7 Card Stud', '7 Card Stud Hi/Lo')
GROUP BY hp.PlayerNm, hp.GameTypCd, hp.BetTypCd;

/*==============================================================*/
/* View: RptTournamentSummary                                   */
/*==============================================================*/
create view RptTournamentSummary as
SELECT tp.PlayerId, tp.PlayerNm, t.BuyInAmt, t.GameTypCd, t.BetTypCd, t.TournamentTypCd, 
COUNT(t.TournamentId) AS CtTourneyNum, 
SUM(t.BuyInAmt) + SUM(t.FeeAmt) AS TotalTourneyCost,
SUM(tp.WinningAmt) AS SumWinningAmt, 
SUM(tp.WinningAmt) - (SUM(t.BuyInAmt) + SUM(t.FeeAmt)) AS NetWinningAmt, 
CASE WHEN SUM(t.BuyInAmt) + SUM(t.FeeAmt) = 0 THEN 0 ELSE 100*((SUM(tp.WinningAmt) - (SUM(t.BuyInAmt) + SUM(t.FeeAmt)))/(SUM(t.BuyInAmt) + SUM(t.FeeAmt))) END AS PctReturnOnInv, 
SUM(CASE WHEN tp.WinningAmt > 0 THEN 1 ELSE 0 END) AS NumTimesInMoney,
((SUM(CAST(CASE WHEN tp.WinningAmt > 0 THEN 1 ELSE 0 END AS DECIMAL(13,2)))/COUNT(t.TournamentId)))*100 AS PctTimesInMoney, 
SUM(t.TotalPlayerNum)/COUNT(t.TournamentId) AS AvgNumPlayers,
SUM(tp.PlacementNum)/COUNT(t.TournamentId) AS AvgPlacement, 
SUM(CASE WHEN tp.PlacementNum = 1 THEN 1 ELSE 0 END) AS Ct1stPlace, 
SUM(CASE WHEN tp.PlacementNum = 2 THEN 1 ELSE 0 END) AS Ct2ndPlace, 
SUM(CASE WHEN tp.PlacementNum = 3 THEN 1 ELSE 0 END) AS Ct3rdPlace,
SUM(tp.SatelliteSeatFlg) AS CtSatelliteSeat
FROM Tournament t 
INNER JOIN TournamentPlayer tp ON t.TournamentId = tp.TournamentId
GROUP BY tp.PlayerId, tp.PlayerNm, t.BuyInAmt, t.GameTypCd, t.BetTypCd, t.TournamentTypCd;

/*==============================================================*/
/* View: TournamentSummaryView                                  */
/*==============================================================*/
create view TournamentSummaryView as
SELECT     tp1.TournamentPlayerId, 
           tp1.PlayerId, 
           tp1.PlayerNm, 
           t1.TournamentId, 
           t1.BuyInAmt, 
           t1.FeeAmt, 
           t1.TournamentTypCd, 
           t1.BetTypCd, 
           t1.GameTypCd, 
           tp1.WinningAmt, 
           t1.TotalPlayerNum, 
           tp1.PlacementNum, 
           t1.StartDt, 
           t1.EndDt,
           tp1.SatelliteSeatFlg
FROM         Tournament t1 INNER JOIN
                      TournamentPlayer tp1 ON t1.TournamentId = tp1.TournamentId;

/*==============================================================*/
/* View: rptBankroll                                            */
/*==============================================================*/
CREATE VIEW rptBankroll AS
SELECT DATE_PART('dow', StartDt) AS weekdaydt,
       DATE_PART('day', StartDt) AS dayofmonthdt, 
       DATE_PART('month', StartDt) AS monthdt, 
       DATE_PART('year', StartDt) AS yeardt, 
       playerbankroll.*
FROM PlayerBankroll;

-- delete from report;
-- delete from reportsection;

INSERT INTO NextId (NextId) VALUES (1);

INSERT INTO Report (ReportId, ReportNm, ReportPage) VALUES (1,'Starting Hand Summary','StartHand.htm');
INSERT INTO Report (ReportId, ReportNm, ReportPage) VALUES (2,'Tournament Summary','Tournament.htm');
INSERT INTO Report (ReportId, ReportNm, ReportPage) VALUES (3,'Action Report','Action.htm');
INSERT INTO Report (ReportId, ReportNm, ReportPage) VALUES (4,'Bankroll - Ring Games by Limit and Bet Type','BRRing.htm');
INSERT INTO Report (ReportId, ReportNm, ReportPage) VALUES (5,'Bankroll - Daily Statistics','BRDaily.htm');
INSERT INTO Report (ReportId, ReportNm, ReportPage) VALUES (6,'Bankroll - Monthly Statistics','BRMonthly.htm');

INSERT INTO ReportSection (SectionId, ReportId, SectionNm, SectionSQL, SectionPage, 
CritHandComboFlg,CritPositionFlg,CritNumPlayerFlg,CritGameTypFlg,CritBetTypFlg,CritTournamentTypFlg,
CritPlayerNmFlg,CritDateRangeFlg,CritLimitRangeFlg, CritTableTypeFlg,CritTounamendIdFlg,CritHandIdFlg, OrderBy)
VALUES (1,1,'starthandsummary','select handcombotxt, suitedstscd, cttimes, pctwinamt, sumtotalwin, netwinamt, avgwinamt, 
avgbbperhand, ctblindtimes, pctvoluntarymoneyinpot, pctmoneywonnoflop, pctpreflopraise, 
pctraisefirstin, pctwenttoshowdown, pctwonshowdown
from rptstarthandgeneral where 0=0 {where} {orderby} limit 100','StartHand_top100.htm',0,0,0,0,0,0,1,0,0,0,0,2,
'1 | Hand,2 | Count,3 | Win%,4 | Winnings,5 | Net Winnings,6 | Avg Winnings,7 | BB/hand, 8 | Blind, 9 | VP$IP%, 10 | MWWF%, 11 |PFR%, 
12 | RFI%, 13 | See Showdown %, 14 | Win Showdown %');

INSERT INTO ReportSection (SectionId, ReportId, SectionNm, SectionSQL, SectionPage, 
CritHandComboFlg,CritPositionFlg,CritNumPlayerFlg,CritGameTypFlg,CritBetTypFlg,CritTournamentTypFlg,
CritPlayerNmFlg,CritDateRangeFlg,CritLimitRangeFlg, CritTableTypeFlg,CritTounamendIdFlg,CritHandIdFlg, OrderBy)
VALUES (2,2,'tournamentsummary','SELECT tp.PlayerNm, 
COUNT(t.TournamentId) AS CtTourneyNum, 
SUM(t.BuyInAmt) + SUM(t.FeeAmt) AS TotalTourneyCost,
SUM(tp.WinningAmt) AS SumWinningAmt, 
SUM(tp.WinningAmt) - (SUM(t.BuyInAmt) + SUM(t.FeeAmt)) AS NetWinningAmt, 
CASE WHEN SUM(t.BuyInAmt) + SUM(t.FeeAmt) = 0 THEN 0 ELSE ROUND(100*((SUM(tp.WinningAmt) - (SUM(t.BuyInAmt) + SUM(t.FeeAmt)))/(SUM(t.BuyInAmt) + SUM(t.FeeAmt))), 3) END AS PctReturnOnInv, 
ROUND(((SUM(CAST(CASE WHEN tp.WinningAmt > 0 THEN 1 ELSE 0 END AS DECIMAL(13,2)))/COUNT(t.TournamentId)))*100, 3) AS PctTimesInMoney, 
SUM(t.TotalPlayerNum)/COUNT(t.TournamentId) AS AvgNumPlayers,
SUM(tp.PlacementNum)/COUNT(t.TournamentId) AS AvgPlacement, 
SUM(CASE WHEN tp.PlacementNum = 1 THEN 1 ELSE 0 END) AS Ct1stPlace, 
SUM(CASE WHEN tp.PlacementNum = 2 THEN 1 ELSE 0 END) AS Ct2ndPlace, 
SUM(CASE WHEN tp.PlacementNum = 3 THEN 1 ELSE 0 END) AS Ct3rdPlace,
SUM(CASE WHEN (cast(t.TotalPlayerNum as decimal(13,2))-cast(tp.PlacementNum as decimal(13,2)))/(cast(t.TotalPlayerNum as decimal(13,2))-1.00)>0.9 THEN 1 ELSE 0 END) AS Pct10Finish,
SUM(CASE WHEN (cast(t.TotalPlayerNum as decimal(13,2))-cast(tp.PlacementNum as decimal(13,2)))/(cast(t.TotalPlayerNum as decimal(13,2))-1.00)>0.85 AND (cast(t.TotalPlayerNum as decimal(13,2))-cast(tp.PlacementNum as decimal(13,2)))/(cast(t.TotalPlayerNum as decimal(13,2))-1.00) < 0.9 THEN 1 ELSE 0 END) AS Pct15Finish,
SUM(CASE WHEN (cast(t.TotalPlayerNum as decimal(13,2))-cast(tp.PlacementNum as decimal(13,2)))/(cast(t.TotalPlayerNum as decimal(13,2))-1.00)>0.8 AND (cast(t.TotalPlayerNum as decimal(13,2))-cast(tp.PlacementNum as decimal(13,2)))/(cast(t.TotalPlayerNum as decimal(13,2))-1.00) < 0.85 THEN 1 ELSE 0 END) AS Pct20Finish,
SUM(CASE WHEN (cast(t.TotalPlayerNum as decimal(13,2))-cast(tp.PlacementNum as decimal(13,2)))/(cast(t.TotalPlayerNum as decimal(13,2))-1.00)>0.7 AND (cast(t.TotalPlayerNum as decimal(13,2))-cast(tp.PlacementNum as decimal(13,2)))/(cast(t.TotalPlayerNum as decimal(13,2))-1.00) < 0.8 THEN 1 ELSE 0 END) AS Pct30Finish,
SUM(CASE WHEN (cast(t.TotalPlayerNum as decimal(13,2))-cast(tp.PlacementNum as decimal(13,2)))/(cast(t.TotalPlayerNum as decimal(13,2))-1.00)>0.6 AND (cast(t.TotalPlayerNum as decimal(13,2))-cast(tp.PlacementNum as decimal(13,2)))/(cast(t.TotalPlayerNum as decimal(13,2))-1.00) < 0.7 THEN 1 ELSE 0 END) AS Pct40Finish,
SUM(CASE WHEN (cast(t.TotalPlayerNum as decimal(13,2))-cast(tp.PlacementNum as decimal(13,2)))/(cast(t.TotalPlayerNum as decimal(13,2))-1.00)>0.5 AND (cast(t.TotalPlayerNum as decimal(13,2))-cast(tp.PlacementNum as decimal(13,2)))/(cast(t.TotalPlayerNum as decimal(13,2))-1.00) < 0.6 THEN 1 ELSE 0 END) AS Pct50Finish,
SUM(tp.SatelliteSeatFlg) AS CtSatelliteSeat
FROM Tournament t 
INNER JOIN TournamentPlayer tp ON t.TournamentId = tp.TournamentId
where 0=0 {where}
GROUP BY tp.PlayerNm
{orderby} LIMIT 100','Tournament_Summary.htm',2,2,2,0,0,0,0,0,0,2,2,2,
'1 | PlayerName,2 | Count,3 | Cost,4 | Winnings,5 | Net Winnings,6 | ROI%,7 | ITM%,8 | Avg Player Count, 9| Avg Place,10 | 1sts,11 | 2nds,12 | 3rds,13 | Top15%,14 | Top20%,15 | Top30%,16 | Top40%,17 | Top50%,18 | Sat Seat');

INSERT INTO ReportSection (SectionId, ReportId, SectionNm, SectionSQL, SectionPage, 
CritHandComboFlg,CritPositionFlg,CritNumPlayerFlg,CritGameTypFlg,CritBetTypFlg,CritTournamentTypFlg,
CritPlayerNmFlg,CritDateRangeFlg,CritLimitRangeFlg,CritTableTypeFlg,CritTounamendIdFlg,CritHandIdFlg, OrderBy)
VALUES (3,1,'playersummary','select handplayer.playernm, count(handplayer.handplayerid) as CTHands,
ROUND(100*(sum(cast(case when handplayer.preflopraiseflg = 1 then 1 else 0 end as decimal(13,2)))/count(handplayerid)), 3) as pctPFR,
ROUND(100*(sum(cast(case when handplayer.preflopraiseflg = 1 AND handplayer.firstinflg = 1 AND handplayer.positionnum IN (1,2) then 1 else 0 end as decimal(13,2)))/count(handplayerid)), 3) as pctBlindSteal,
ROUND(100*(sum(cast(case when handplayer.voluntarymoneyflg = 1 then 1 else 0 end as decimal(13,2)))/count(handplayerid)), 3) as pctVP$IP
from handplayer 
inner join tournamenthand on handplayer.handid = tournamenthand.handid 
where 0 = 0 {where}
group by handplayer.playernm
{orderby} limit 100','StartHand_playersummary.htm',0,0,0,0,0,0,1,0,0,0,0,2,
'1 | Player Name,2 | Count,3 | PFR%,4 | Blind Steal %,5 | VP$IP %');

INSERT INTO ReportSection (SectionId, ReportId, SectionNm, SectionSQL, SectionPage, 
CritHandComboFlg,CritPositionFlg,CritNumPlayerFlg,CritGameTypFlg,CritBetTypFlg,CritTournamentTypFlg,
CritPlayerNmFlg,CritDateRangeFlg,CritLimitRangeFlg,CritTableTypeFlg,CritTounamendIdFlg,CritHandIdFlg, OrderBy)
VALUES (4,3,'action','select handplayer.playernm, count(handplayer.handplayerid) as CTHands,
ROUND(100*(sum(cast(case when handplayer.preflopraiseflg = 1 then 1 else 0 end as decimal(13,2)))/count(handplayerid)), 3) as pctPFR,
CASE WHEN sum(case when handplayer.positionnum in (1,2) then 1 else 0 end) >0 
     THEN ROUND(100*(sum(cast(case when handplayer.preflopraiseflg = 1 AND handplayer.firstinflg = 1 AND handplayer.positionnum IN (1,2) then 1 else 0 end as decimal(13,2)))/sum(cast(CASE WHEN handplayer.positionnum IN (1,2) THEN 1 ELSE 0 END AS DECIMAL(13,2)))), 3) 
     ELSE 0 END as pctBlindSteal,
ROUND(100*(sum(cast(case when handplayer.voluntarymoneyflg = 1 then 1 else 0 end as decimal(13,2)))/count(handplayerid)), 3) as pctVP$IP
from handplayer
inner join tournamenthand on handplayer.handid = tournamenthand.handid 
where 0=0 {where}
group by handplayer.playernm {orderby} LIMIT 100','Action_summary.htm',0,0,0,0,0,0,1,0,0,0,0,2,
'1 | Player Name,2 | Count,3 | PFR%,4 | Blind Steal %,5 | VP$IP %');

INSERT INTO ReportSection (SectionId, ReportId, SectionNm, SectionSQL, SectionPage, 
CritHandComboFlg,CritPositionFlg,CritNumPlayerFlg,CritGameTypFlg,CritBetTypFlg,CritTournamentTypFlg,
CritPlayerNmFlg,CritDateRangeFlg,CritLimitRangeFlg, CritTableTypeFlg,CritTounamendIdFlg,CritHandIdFlg, OrderBy)
VALUES (5, 4, 'brringstats', 'select gametypcd, bettypcd, BBAmt, sum(handct) as totalhands, sum(netchangeamt) as netprofit,
sum(nettimeamt)/60 as totalhours,
sum(netchangeamt)/(sum(nettimeamt)/60) as avgperhour, 
(sum(netchangeamt/BBAmt)/(sum(nettimeamt)/60)) as avgBBperhour,
100*sum(netchangeamt)/sum(handct) as avgper100hands,
100*(sum(netchangeamt/BBAmt)/sum(handct)) as avgBBper100hands 
from playerbankroll 
where  typecd = ''ring game'' and bbamt is not null {where}
group by gametypcd, bettypcd, BBAmt
{orderby}', 'BRRing_Stats.htm', 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, '1|Game Type, 2|Bet Type, 3|Group, 4|Count, 5|Net Profit, 6|Total Hours, 7|Hourly Rate, 8|BB per hour, 9|$ per 100 hands, 10|BB per 100 hands');

INSERT INTO ReportSection (SectionId, ReportId, SectionNm, SectionSQL, SectionPage, 
CritHandComboFlg,CritPositionFlg,CritNumPlayerFlg,CritGameTypFlg,CritBetTypFlg,CritTournamentTypFlg,
CritPlayerNmFlg,CritDateRangeFlg,CritLimitRangeFlg, CritTableTypeFlg,CritTounamendIdFlg,CritHandIdFlg, OrderBy)
VALUES (6,5,'brdailyoverall','SELECT yeardt, monthdt, dayofmonthdt, count(monthdt) as ctrecords,
sum(nettimeamt)/60 as totaltime, sum(netchangeamt) as totalchange, sum(netchangeamt)/(sum(nettimeamt)/60) as changeperhour,
sum(handct) as totalhands, 100*sum(netchangeamt)/sum(handct) as changeper100
FROM rptBankroll
WHERE 0=0 {where}
GROUP BY yeardt, monthdt, dayofmonthdt {orderby}','BRDaily_overall.htm',0,0,0,0,0,0,0,0,0,0,0,0,
'1 | Year,2 | Month,3 | Day,4 | #Sessions,5 | Hours,6 | Net Change,7 | HourlyRate, 8 | #Hands, 9 | $/100');

INSERT INTO ReportSection (SectionId, ReportId, SectionNm, SectionSQL, SectionPage, 
CritHandComboFlg,CritPositionFlg,CritNumPlayerFlg,CritGameTypFlg,CritBetTypFlg,CritTournamentTypFlg,
CritPlayerNmFlg,CritDateRangeFlg,CritLimitRangeFlg, CritTableTypeFlg,CritTounamendIdFlg,CritHandIdFlg, OrderBy)
VALUES (7,5,'brdailytype','SELECT yeardt, monthdt, dayofmonthdt, typecd, count(monthdt) as ctrecords,
sum(nettimeamt)/60 as totaltime, sum(netchangeamt) as totalchange, sum(netchangeamt)/(sum(nettimeamt)/60) as changeperhour,
sum(handct) as totalhands, 100*sum(netchangeamt)/sum(handct) as changeper100
FROM rptBankroll
WHERE 0=0 {where}
GROUP BY yeardt, monthdt, dayofmonthdt, typecd {orderby}','BRDaily_type.htm',0,0,0,0,0,0,0,0,0,0,0,0,
'1 | Year,2 | Month,3 | Day,4 | Type, 5 | #Sessions,6 | Hours,7 | Net Change,8 | HourlyRate,9 | #Hands,10 | $/100');

INSERT INTO ReportSection (SectionId, ReportId, SectionNm, SectionSQL, SectionPage, 
CritHandComboFlg,CritPositionFlg,CritNumPlayerFlg,CritGameTypFlg,CritBetTypFlg,CritTournamentTypFlg,
CritPlayerNmFlg,CritDateRangeFlg,CritLimitRangeFlg, CritTableTypeFlg,CritTounamendIdFlg,CritHandIdFlg, OrderBy)
VALUES (8,5,'brdailygame','SELECT yeardt, monthdt, dayofmonthdt, typecd, gametypcd, bettypcd, BBAmt, count(monthdt) as ctrecords,
sum(nettimeamt)/60 as totaltime, sum(netchangeamt) as totalchange, sum(netchangeamt)/(sum(nettimeamt)/60) as changeperhour,
sum(handct) as totalhands, 100*sum(netchangeamt)/sum(handct) as changeper100
FROM rptBankroll
WHERE 0=0 {where}
GROUP BY yeardt, monthdt, dayofmonthdt, typecd, gametypcd, bettypcd, BBAmt {orderby}','BRDaily_game.htm',0,0,0,0,0,0,0,0,0,0,0,0,
'1 | Year,2 | Month,3 | Day,4 | Type, 5 | Game, 6 | BetType, 7 | BBAmt, 8 | #Sessions,9 | Hours,10 | Net Change,11 | HourlyRate,12 | #Hands,13 | $/100');

INSERT INTO ReportSection (SectionId, ReportId, SectionNm, SectionSQL, SectionPage, 
CritHandComboFlg,CritPositionFlg,CritNumPlayerFlg,CritGameTypFlg,CritBetTypFlg,CritTournamentTypFlg,
CritPlayerNmFlg,CritDateRangeFlg,CritLimitRangeFlg, CritTableTypeFlg,CritTounamendIdFlg,CritHandIdFlg, OrderBy)
VALUES (9,6,'brmonthlygame','SELECT yeardt, monthdt, typecd, gametypcd, bettypcd, BBAmt, count(monthdt) as ctrecords,
sum(nettimeamt)/60 as totaltime, sum(netchangeamt) as totalchange, sum(netchangeamt)/(sum(nettimeamt)/60) as changeperhour,
sum(handct) as totalhands, 100*sum(netchangeamt)/sum(handct) as changeper100
FROM rptBankroll
WHERE 0=0 {where}
GROUP BY yeardt, monthdt, typecd, gametypcd, bettypcd, BBAmt {orderby}','BRMonthly_game.htm',0,0,0,0,0,0,0,0,0,0,0,0,
'1 | Year,2 | Month,3 | Type, 4 | Game, 5 | BetType, 6 | BBAmt, 7 | #Sessions,8 | Hours,9 | Net Change,10 | HourlyRate,11 | #Hands,12 | $/100');

INSERT INTO ReportSection (SectionId, ReportId, SectionNm, SectionSQL, SectionPage, 
CritHandComboFlg,CritPositionFlg,CritNumPlayerFlg,CritGameTypFlg,CritBetTypFlg,CritTournamentTypFlg,
CritPlayerNmFlg,CritDateRangeFlg,CritLimitRangeFlg, CritTableTypeFlg,CritTounamendIdFlg,CritHandIdFlg, OrderBy)
VALUES (10,6,'brmonthlytype','SELECT yeardt, monthdt, typecd, count(monthdt) as ctrecords,
sum(nettimeamt)/60 as totaltime, sum(netchangeamt) as totalchange, sum(netchangeamt)/(sum(nettimeamt)/60) as changeperhour,
sum(handct) as totalhands, 100*sum(netchangeamt)/sum(handct) as changeper100
FROM rptBankroll 
WHERE 0=0 {where}
GROUP BY yeardt, monthdt, typecd {orderby}','BRMonthly_type.htm',0,0,0,0,0,0,0,0,0,0,0,0,
'1 | Year,2 | Month,3 | Type, 4 | #Sessions,5 | Hours,6 | Net Change,7 | HourlyRate,8 | #Hands,9 | $/100');

INSERT INTO ReportSection (SectionId, ReportId, SectionNm, SectionSQL, SectionPage, 
CritHandComboFlg,CritPositionFlg,CritNumPlayerFlg,CritGameTypFlg,CritBetTypFlg,CritTournamentTypFlg,
CritPlayerNmFlg,CritDateRangeFlg,CritLimitRangeFlg, CritTableTypeFlg,CritTounamendIdFlg,CritHandIdFlg, OrderBy)
VALUES (11,6,'brmonthlyoverall','SELECT yeardt, monthdt, count(monthdt) as ctrecords,
sum(nettimeamt)/60 as totaltime, sum(netchangeamt) as totalchange, sum(netchangeamt)/(sum(nettimeamt)/60) as changeperhour,
sum(handct) as totalhands, 100*sum(netchangeamt)/sum(handct) as changeper100
FROM rptBankroll
WHERE 0 = 0 {where}
GROUP BY yeardt, monthdt {orderby}','BRMonthly_overall.htm',0,0,0,0,0,0,0,0,0,0,0,0,
'1 | Year,2 | Month,3 | #Sessions,4 | Hours,5 | Net Change,6 | HourlyRate,7 | #Hands,8 | $/100');
