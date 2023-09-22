<script lang="ts">
	import type { GameDto } from '../../nlp-api';

	export let game: GameDto;
</script>

<div class="game">
	<div class="title">
		{game.gameName}
	</div>
</div>
