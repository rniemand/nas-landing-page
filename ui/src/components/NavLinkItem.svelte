<script lang="ts">
	import type { NavbarLink } from "./TopNavigation";
    export let link: NavbarLink;

    const getLinkHref = (_link: NavbarLink) => {
        if(_link.href?.length || 0 > 0) return _link.href;
        return '#!';
    };

    const handleOnClick = (_link: NavbarLink) => {
        if(!_link.onClick) return;
        _link.onClick();
    };
</script>

{#if (link.children && link.children?.length || 0) > 0}
    <li>
        <details>
        <summary>{link.text}</summary>
        <ul class="p-2">
            {#each link.children || [] as childLink}
                <li>
                    <a href={getLinkHref(childLink)} on:click={() => handleOnClick(childLink)}>
                        {childLink.text}
                    </a>
                </li>
            {/each}
        </ul>
        </details>
    </li>
{:else}
    <li>
        <a href={getLinkHref(link)} on:click={() => handleOnClick(link)}>
            {link.text}
        </a>
    </li>
{/if}