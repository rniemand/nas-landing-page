<script lang="ts">
	import { Col, Pagination, PaginationItem, PaginationLink, Row } from 'sveltestrap';
	import type { GameDto } from '../../nlp-api';
	import type { PaginationEntry } from '../../components/Components';

	export let games: GameDto[] = [];
	export let pageSize: number = 10;
	export let onPageChanged: (games: GameDto[]) => void;
	let showPagination: boolean = false;
	let pages: PaginationEntry[] = [];
	let currentPage: PaginationEntry;

	const gamesChanged = (_games: GameDto[]) => {
		showPagination = _games.length > pageSize;
		pages = [];
		for (let i = 0; i < Math.ceil(_games.length / pageSize); i++) {
			pages.push({ active: false, pageNumber: i + 1 });
		}
		currentPage = pages[0];
	};

	const currentPageChanged = (_page: PaginationEntry) => {
		if (!showPagination) return;
		let idxStart = (_page.pageNumber - 1) * pageSize;
		onPageChanged(games.slice(idxStart, idxStart + pageSize));
	};

	$: gamesChanged(games);
	$: currentPageChanged(currentPage);
</script>

{#if showPagination}
	<Row class="mt-2">
		<Col>
			<Pagination class="justify-content-end">
				<PaginationItem disabled={currentPage === pages[0]}>
					<PaginationLink first on:click={() => (currentPage = pages[0])} />
				</PaginationItem>
				{#each pages as page}
					<PaginationItem active={currentPage === page}>
						<PaginationLink on:click={() => (currentPage = page)}>
							{page.pageNumber}
						</PaginationLink>
					</PaginationItem>
				{/each}
				<PaginationItem disabled={currentPage === pages[pages.length - 1]}>
					<PaginationLink last on:click={() => (currentPage = pages[pages.length - 1])} />
				</PaginationItem>
			</Pagination>
		</Col>
	</Row>
{/if}
