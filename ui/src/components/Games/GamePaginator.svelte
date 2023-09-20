<script lang="ts">
	import type { BasicGameInfoDto } from '../../nlp-api';
	import Paginator from '../Common/Collections/Paginator.svelte';
	import PaginatorItem from '../Common/Collections/PaginatorItem.svelte';
	import RnHead from '../Common/RnHead.svelte';

	interface PageInfo {
		pageNumber: number;
		active: boolean;
	}

	export let games: BasicGameInfoDto[] = [];
	export let pageSize: number = 10;
	export let onPaginationChanged: (games: BasicGameInfoDto[]) => void;
	let showPaginator: boolean = false;
	let pages: PageInfo[] = [];

	const gamesChanged = (_games: BasicGameInfoDto[]) => {
		showPaginator = _games.length > pageSize;
		pages = [];
		for (let i = 1; i < Math.ceil(_games.length / pageSize) + 1; i++) {
			pages.push({
				pageNumber: i,
				active: i === 1
			});
		}
		setActivePage(1);
	};

	const setActivePage = (pageNumber: number) => {
		for (const page of pages) {
			page.active = pageNumber === page.pageNumber;
		}
		pages = pages;

		let startIdx = (pageNumber - 1) * pageSize;
		let endIdx = startIdx + pageSize;
		onPaginationChanged(games.slice(startIdx, endIdx));
	};

	$: gamesChanged(games);
</script>

{#if showPaginator}
	<div class="d-flex">
		<RnHead class="me-auto">{games.length} Game(s)</RnHead>

		<Paginator>
			{#each pages as page}
				<PaginatorItem on:click={() => setActivePage(page.pageNumber)} active={page.active}
					>{page.pageNumber}</PaginatorItem>
			{/each}
		</Paginator>
	</div>
{/if}
