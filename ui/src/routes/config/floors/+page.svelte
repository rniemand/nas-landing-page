<script lang="ts">
	import { Accordion, AccordionItem, Button, Col, Row } from 'sveltestrap';
	import { AppUrls, ConfigUrls } from '../../../enums/AppUrls';
	import { CoreClient, type HomeFloorDto } from '../../../nlp-api';
	import Spinner from '../../../components/common/Spinner.svelte';
	import AddFloorModal from './AddFloorModal.svelte';
	import { goto } from '$app/navigation';
	import EditFloorModal from './EditFloorModal.svelte';
	import NavigationCrumbs from '../../../components/core/NavigationCrumbs.svelte';
	import NavigationCrumb from '../../../components/core/NavigationCrumb.svelte';

	let floors: HomeFloorDto[] = [];
	let loading: boolean = true;
	let editModal: EditFloorModal;

	const refreshFloors = async () => {
		loading = true;
		// TODO: work in the users home id
		floors = (await new CoreClient().getFloors(1)) || [];
		loading = false;
	};

	const onFloorAdded = () => refreshFloors();
	const onFloorUpdated = () => refreshFloors();

	refreshFloors();
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
					<AccordionItem header={floor.floorName}>
						{floor.floorName}
						<div class="text-end">
							<Button color="success" on:click={() => editModal.editFloor(floor)}>Edit</Button>
							<Button color="primary" on:click={() => goto(ConfigUrls.FloorRooms(floor.floorId))}>
								Rooms
							</Button>
						</div>
					</AccordionItem>
				{/each}
			</Accordion>
		{/if}
	</Col>
</Row>
