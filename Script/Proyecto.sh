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

showreport(){
	clear
	cd ..
	cd Informe
	xdg-open Informe.pdf
}

showslides(){
	clear
	cd ..
	cd Presentacion
	xdg-open Presentacion.pdf
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

while true; do
	clear
	echo "Escoge la opcion que desees"
	echo "1) run"
	echo "2) report"
	echo "3) slides"
	echo "4) show report"
	echo "5) show slides"
	echo "6) clean"

	read option

	case $option in
		"1")
			run
			read
			;;
		"2")
			report
			read
			;;
		"3")	
			slides
			read
			;;
		"4")
			showreport
			read
			;;
		"5")
			showslides
			read
			;;
		"6")	
			clean
			read
			;;

	esac
done

