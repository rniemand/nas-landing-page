<script lang="ts">
	import { Accordion, AccordionItem, Col, Row } from 'sveltestrap';
	import { AppUrls, ConfigUrls } from '../../../enums/AppUrls';
	import { FloorClient, type HomeFloorDto } from '../../../nlp-api';
	import Spinner from '../../../components/common/Spinner.svelte';
	import AddFloorModal from './AddFloorModal.svelte';
	import { goto } from '$app/navigation';
	import EditFloorModal from './EditFloorModal.svelte';
	import NavigationCrumbs from '../../../components/core/NavigationCrumbs.svelte';
	import NavigationCrumb from '../../../components/core/NavigationCrumb.svelte';
	import FloorInfoDisplay from './FloorInfoDisplay.svelte';
	import { page } from '$app/stores';

	let floors: HomeFloorDto[] = [];
	let loading: boolean = true;
	let editModal: EditFloorModal;
	let floorId: number = parseInt($page.url.searchParams.get('floorId') || '0');

	const refreshFloors = async () => {
		loading = true;
		floors = (await new FloorClient().listFloors()) || [];
		loading = false;
	};

	const onFloorAdded = () => refreshFloors();
	const onFloorUpdated = () => refreshFloors();
	const onEdit = (floor: HomeFloorDto) => editModal.editFloor(floor);
	const onViewRooms = (floor: HomeFloorDto) => goto(ConfigUrls.FloorRooms(floor.floorId));

	refreshFloors();

	$: floorId = parseInt($page.url.searchParams.get('floorId') || '0');
</script>

<NavigationCrumbs>
	<NavigationCrumb icon="bi-house-fill" url={AppUrls.Home} />
	<NavigationCrumb icon="bi-gear-fill" url={ConfigUrls.Root} />
	<NavigationCrumb title="Floors" />
</NavigationCrumbs>

<Row>
	<Col>
		<Spinner show={loading} />
		<div class="text-end">
			<AddFloorModal disabled={loading} {onFloorAdded} />
			<EditFloorModal bind:this={editModal} {onFloorUpdated} />
		</div>

		{#if floors.length > 0}
			<Accordion class="mt-3">
				{#each floors as floor}
					<AccordionItem header={floor.floorName} active={floorId === floor.floorId}>
						<FloorInfoDisplay {floor} {onEdit} {onViewRooms} />
					</AccordionItem>
				{/each}
			</Accordion>
		{/if}
	</Col>
</Row>
