1 de Janeiro, 2005


Ultima Online Client Hacker (UOCH) 2.1
Developer's edition release

Copyright 2005 Daniel 'Necr0Potenc3' Cavalcanti


[como Usar]
1 - rode o UOCH.exe
2 - ele autodetecta o client do uo no registro. mas voce pode escolher outro client
com o botao "..."
3 - selecione os patches que voce quer (Developer dump cancela todos os patches)
4 - click no botao "patch"
5 - um novo client chamado "UOCH_n0p3_Clientxxx.exe" vai ser criado na pasta do
client que voce deu patch. o xxx e o numero de identificacao, voce pode criar&patch
em quantos clients voce quiser

[Características]
- Código fonte aberto
- Developer's dump: Mostra detalhes do client como: loginkeys, CRC32, size, time datestamp,
image base, entry point, version, offset da funcao de encriptacao, offset do
packet handler, offset da tabela de pacotes, dump da tabela de pacotes em
formato C.

- Everman's way: Protecao contra seu personagem ser deletado, cada vez que voce
tentar deletar seu personagem o client vai dar crash como uma forma de te avisar.

- Multi clients: Permite que voce abra mais do que um client ao mesmo tempo.
(obs: clients mais novos permitem apenas 2 clients ao mesmo tempo)

- Clear nights: Tambem conhecido como NightSight, Night hack. Voce nunca mais
vai ter noites durante o jogo.

- Special names: Permite a criacao de personagens com nomes curtos (ou sem nome),
e nomes comecando por Lord, Lady, GM, Counselor, seer.

- Remove encryption: Remove a encriptacao do client e informa voce qual encriptacao
esta sendo removida. (Login, Blowfish, Twofish, MD5)

- Remove macro delay: Remove o atraso entre a macro arm/disarm.

- Remove stamina check: Permite que voce ande atraves das pessoas mesmo quando
sua estamina nao esta cheia. Modo Hooligan!!


[Codigo fonte]
   E distribuido com o executavel e voce e livre para altera-lo como voce quiser.
Mas me de os creditos, blz?

   O codigo fonte contem "CrackTools.c". Uma coletanea de funcoes, que eu
desenvolvi, uteis para cracking.
OBS: Todas as funcoes retornam o offset dentro do buffer e nao o endereco virtual
(image base + offset)