0xc000 CONSTANT HERESTART ( Start of ram )

0xdfff CONSTANT PS_START ( End of ram )
0xdf00 CONSTANT RS_START ( 0xff back )

RS_START 0xb0 - CONSTANT SYSVARS ( Store the sysvars 0xb0 before the stack )

SYSVARS 0xa0 + CONSTANT SERIAL_BUFFER ( 0xa long buffer )
\ SYSVARS 0xaa + CONSTANT PS2_MEM

( high low -- checksum )
: CHECKSUM 0 ROT> DO I C@ - 1- 255 AND LOOP ; 
: DUMP DO I C@ DUP DUP .x ." :" EMIT NL> LOOP ;

60 LOAD ( LR35902 assembler )
: nNOP, 0 DO NOP, LOOP ;
: VECNT 3 PC + JP, 4 nNOP, RETI, ; ( Writes one vector table entry )
: VECTABLE 13 0 DO VECNT LOOP ; ( Creates vector table [must be a function or else DO LOOPs will not work] )

( TODO - Move this to a hardware def block )
: rLY 0xff44 ;
: rLCDC 0xff40 ;
: rBGP 0xff47 ;
: rSCY 0xff42 ;
: rSCX 0xff43 ;
: rNR52 0xff26 ;
( Move this somewhere ??? )
VARIABLE lblexec VARIABLE lbluflw
VARIABLE lbloflw?
VARIABLE EL1 VARIABLE EL2 VARIABLE EL3
VARIABLE EL4 VARIABLE EL5 VARIABLE EL6
VARIABLE FontStart
VARIABLE HelloWorldStr
2048 CONSTANT FontLength

264 LOAD
263 LOAD ( font compiler )

\ 200 205 LOADR ( xcomp )
HERE ORG !

( Gameboy Header )
( Vector table )
VECTABLE
( 0x68 )

( Padding )
0x98 ALLOT0 ( 0x100 )

NOP, JR, 0x4d C, NOP,  ( jump to addr 0x150 on boot )

( Nintendo Logo )
0xce C, 0xed C, 0x66 C, 0x66 C, 0xcc C, 0x0d C, 0x00 C, 0x0b C, 0x03 C, 0x73 C, 0x00 C, 0x83 C, 0x00 C, 0x0c C, 0x00 C, 0x0d C,
0x00 C, 0x08 C, 0x11 C, 0x1f C, 0x88 C, 0x89 C, 0x00 C, 0x0e C, 0xdc C, 0xcc C, 0x6e C, 0xe6 C, 0xdd C, 0xdd C, 0xd9 C, 0x99 C,
0xbb C, 0xbb C, 0x67 C, 0x63 C, 0x6e C, 0x0e C, 0xec C, 0xcc C, 0xdd C, 0xdc C, 0x99 C, 0x9f C, 0xbb C, 0xb9 C, 0x33 C, 0x3e C,

," COLLAPSE OS" ( Title )
," AREJ" ( Manufacturer )
0xc0 C, ( CGB Flag )
0x2 ALLOT0 ( Licensee code )
0x00 C, ( SGB Flag )
( TODO: Set proper banking size )
0x00 C, ( Cartridge type )
0x00 C, ( ROM Size )

0x00 C, ( RAM Size )
0x00 C, ( Region )
0x33 C, ( Use 4-char manufacturer code )
0x00 C, ( Version number )

ORG @ 0x14d + ORG @ 0x134 + CHECKSUM C, ( Header checksum )

0x2 ALLOT0  ( Unused Global checksum )

( Start Collapse OS at 0x14F )
0x14f BIN( !
470 474 LOADR ( LR35902 boot code )
\ 210 227 LOADR ( forth low no BLK )
\ ??? ??? LOADR ( serial PS/2 driver )
\ 246 249 LOADR ( PS/2 Key system )
\ 236 239 LOADR ( forth high )
( Reset the ORG pointer so that the resulting ROM binary starts at the start of the header )
\ ORG @ 0x150 - ORG !
