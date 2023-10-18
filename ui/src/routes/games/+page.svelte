<script lang="ts">
	import GamePlatforms from './GamePlatforms.svelte';
	import type { GamePlatformDto } from '../../nlp-api';
	import PlatformGames from './PlatformGames.svelte';
	import NavigationCrumbs from '../../components/core/NavigationCrumbs.svelte';
	import NavigationCrumb from '../../components/core/NavigationCrumb.svelte';
	import { AppUrls } from '../../enums/AppUrls';

	let platform: GamePlatformDto | undefined = undefined;
</script>

<NavigationCrumbs>
	<NavigationCrumb icon="bi-house-fill" url={AppUrls.Home} />
	<NavigationCrumb title="Games" />
</NavigationCrumbs>

<GamePlatforms bind:value={platform} />
<PlatformGames {platform} />
