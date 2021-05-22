# See /doc/hw/LR35902/Gameboy.txt
TARGET = os.bin
BASE = ../../..
CDIR = $(BASE)/cvm
BLKPACK = $(BASE)/tools/blkpack
STAGE = $(CDIR)/stage
EMUL = $(BASE)/emul/gameboy/bgb64.exe

.PHONY: all
all: $(TARGET) 
os.bin: xcomp.fs $(STAGE) 
	$(STAGE) < xcomp.fs > $@

$(STAGE):
	$(MAKE) -C $(CDIR) stage

.PHONY: emul
emul: $(TARGET) $(EMUL)
	$(EMUL) $(TARGET)

.PHONY: clean
clean:
	rm -f *.bin 
