0xc000 CONSTANT HERESTART ( Start of ram )

0xdfff CONSTANT PS_ADDR ( End of ram )
0xdeff CONSTANT RS_ADDR ( 0x100 back )

RS_ADDR 0xb0 - CONSTANT SYSVARS ( Store the sysvars 0xb0 before the stack )

SYSVARS 0xa0 + CONSTANT SERIAL_BUFFER ( 0xa long buffer)
SYSVARS 0xaa + CONSTANT PS2_MEM

( high low -- checksum )
: CHECKSUM 0 ROT> DO I C@ - 1- 255 AND LOOP ; 
: DUMP DO I C@ DUP DUP .x ." :" EMIT NL> LOOP ;

5 LOAD ( LR35902 assembler )
280 LOAD
200 205 LOADR ( xcomp )
HERE ORG !

( Gameboy Header )
0x40 ALLOT0
( TODO: Could we put the ABI in these first 40 bytes? )
( TODO: Interrupt vector )
0x20 ALLOT0 ( 0x60 )

( Padding )
0xa0 ALLOT0 ( 0x100 )

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
\ 0x14f BIN( !
\ 281 300 LOADR ( boot.z80 )
\ ??? ??? LOADR ( LR35902  core )
\ 210 227 LOADR ( forth low no BLK )
\ ??? ??? LOADR ( serial PS/2 driver )
\ 246 249 LOADR ( PS/2 Key system )
\ 236 239 LOADR ( forth high )
NOP,
A 100 LDri,
25 ADDi,
HALT,
( Reset the ORG pointer so that the resulting ROM binary starts at the start of the header )
\ ORG @ 0x150 - ORG !
