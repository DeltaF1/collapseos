( TRS-80 Color Computer 2 )
50 LOAD ( 6809 assembler )
3 CONSTS PS_ADDR 0x8000 RS_ADDR 0x7f00 HERESTART 0x0600
RS_ADDR 0xb0 - CONSTANT SYSVARS
SYSVARS 0xa0 + CONSTANT GRID_MEM
0xc000 BIN( !
450 LOAD ( boot.6809 declarations )
200 205 LOADR ( xcomp )
451 459 LOADR ( boot.6809 )
210 227 LOADR ( forth low, no BLK )
461 463 LOADR ( drivers )
240 241 LOADR ( Grid )
236 239 LOADR ( forth high )
XWRAP" GRID$ "
