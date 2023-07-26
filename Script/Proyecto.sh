run(){
	cd ..
	dotnet watch run --project MoogleServer
	clear
	cd Script
}

report(){
	cd ..
	cd Informe
	latexmk -pdf Informe.tex
	clear
	cd ..
	cd Script
}

slides(){
	cd ..
	cd Presentacion
	latexmk -pdf Presentacion.tex
	clear
	cd ..
	cd Script
}

show_report(){
	clear
	cd ..
	cd Informe
	if [ -z "$1" ]
	then
		xdg-open Informe.pdf
	else
		$1 Informe.pdf
	fi
	cd ..
	cd Script
}

show_slides(){
	clear
	cd ..
	cd Presentacion
	if [ -z "$1" ]; 
	then
		xdg-open Presentacion.pdf
	else
		$1 Presentacion.pdf
	fi
	cd ..
	cd Script
}

clean(){
	cd ..
	cd Informe
	rm Informe.aux Informe.fdb_latexmk Informe.fls Informe.log indent.log pdflatex32230.fls Informe.synctex.gz Informe.pdf
	cd ..
	cd Presentacion 
	rm -f Presentacion.aux Presentacion.fdb_latexmk Presentacion.fls Presentacion.log Presentacion.nav Presentacion.snm Presentacion.toc Presentacion.out Presentacion.synctec.gz Presentacion.pdf
	cd ..
	cd Script
}

"$@"