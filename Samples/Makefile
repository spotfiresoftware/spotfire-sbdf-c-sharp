DEPS =
REFS = ..\Spotfire.Dxp.Data.Formats.Sbdf.dll

all:    build

build:
	copy $(REFS) .
	csc /r:$(REFS) /o+ /t:exe /out:ReaderSample.exe  ReaderSample.cs $(DEPS)
	csc /r:$(REFS) /o+ /t:exe /out:WriterSample.exe  WriterSample.cs $(DEPS)

clean:
	del /Q *.exe

