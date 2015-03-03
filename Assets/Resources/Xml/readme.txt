
≪概要≫
「ひよこプロジェクト」のステージ情報は、
同ディレクトリにある「stage.xml」を基に構成しております。
ステージ情報とは、ゲームを構成する最少構成になります。


≪パラメータ詳細≫

	【パラメータ例】
		<data id="0">
		  <InitialUnlock>1</InitialUnlock>
		  <GameMode>0</GameMode>
	      <BackGroundID>999</BackGroundID>
		  <Parameters>
		    <Parameter0>0</Parameter0>
		    <Parameter1>0</Parameter1>
		    <Parameter2>0</Parameter2>
		    <Parameter3>0</Parameter3>
		  </Parameters>
		  <BorderLine>
		    <Level0>10</Level0>
		    <Level1>20</Level1>
		    <Level2>30</Level2>
		  </BorderLine>
		</data>
		
	
	【パラメータ解説】
		・ステージID
			<data id="0">
			～
			</data>
			IDの重複は不可
		
		・初期アンロックフラグ
			<InitialUnlock>1</InitialUnlock>
			初期からアンロックされているステージには"1"を入れておく
			アンロックされていないステージに関しては、"0"を入れる、もしくはパラメータ自体を設定しない
		
		・ゲームモードID
			<GameMode>0</GameMode>
			ステージに対応するゲームモードID
			0 … 制限時間内に特定数のひよこを捕まえる
			1 … ～
			2 … ～
			※TODO
			現状、ゲームモードIDはソースコードへハードコーディングされている
			後々は外部リソース化する
			
		・背景ID
			<BackGroundID>999</BackGroundID>
			ステージに対応する背景ID
			※TODO
			現状、背景IDはソースコードへハードコーディングされている
		
		・ゲームパラメータ
			<Parameters>
			  <Parameter0>0</Parameter0>
			  <Parameter1>0</Parameter1>
			  <Parameter2>0</Parameter2>
			  <Parameter3>0</Parameter3>
			  ～
			</Parameters>
			各ゲームモードに対応したパラメータ
			ゲームモードにより可変長で対応
			
		・クリア条件
			<BorderLine>
			  <Level0>10</Level0>
			  <Level1>20</Level1>
			  <Level2>30</Level2>
			</BorderLine>
			ゲームモードが何であれ、最終出力はスコア(整数数値型)に変換するため、均一のボーダーラインで対応する
			下記に対応
			Level0 … かんたん
			Level1 … ふつう
			Level2 … むずかしい


